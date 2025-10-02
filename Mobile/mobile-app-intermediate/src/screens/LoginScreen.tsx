import React from 'react';
import { View } from 'react-native';
import { Button, TextInput, HelperText, ActivityIndicator, Text } from 'react-native-paper';
import { Formik } from 'formik';
import * as Yup from 'yup';
import { AuthContext } from '../contexts/AuthContext';

const schema = Yup.object({
  email: Yup.string().email('E-mail inválido').required('Obrigatório'),
  password: Yup.string().min(6,'Mín. 6 caracteres').required('Obrigatório'),
});

export default function LoginScreen({navigation}:any){
  const { signIn, loading } = React.useContext(AuthContext) as any;
  return (
    <View style={{ padding:16, gap:8 }}>
      <Text variant="headlineMedium">Entrar</Text>
      <Formik
        initialValues={{email:'', password:''}}
        validationSchema={schema}
        onSubmit={async (v,{setStatus})=>{
          try { await signIn(v.email, v.password); }
          catch (e:any){ setStatus(e?.response?.data?.message || 'Erro ao entrar'); }
        }}>
        {({handleChange, handleBlur, handleSubmit, values, errors, touched, status})=>(
          <>
            <TextInput label="E-mail" value={values.email} onChangeText={handleChange('email')} onBlur={handleBlur('email')} keyboardType="email-address" autoCapitalize="none" />
            <HelperText type="error" visible={touched.email && !!errors.email}>{errors.email}</HelperText>
            <TextInput label="Senha" value={values.password} onChangeText={handleChange('password')} onBlur={handleBlur('password')} secureTextEntry />
            <HelperText type="error" visible={touched.password && !!errors.password}>{errors.password}</HelperText>
            {status ? <HelperText type="error" visible>{status}</HelperText> : null}
            {loading ? <ActivityIndicator /> : <Button mode="contained" onPress={handleSubmit as any}>Entrar</Button>}
            <Button onPress={()=>navigation.navigate('Register')}>Criar conta</Button>
          </>
        )}
      </Formik>
    </View>
  );
}
