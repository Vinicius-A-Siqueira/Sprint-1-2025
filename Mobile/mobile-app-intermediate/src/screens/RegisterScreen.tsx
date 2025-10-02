import React from 'react';
import { View } from 'react-native';
import { Button, TextInput, HelperText, ActivityIndicator, Text } from 'react-native-paper';
import { Formik } from 'formik';
import * as Yup from 'yup';
import { AuthContext } from '../contexts/AuthContext';

const schema = Yup.object({
  name: Yup.string().min(3,'Mín. 3').required('Obrigatório'),
  email: Yup.string().email('E-mail inválido').required('Obrigatório'),
  password: Yup.string().min(6,'Mín. 6').required('Obrigatório'),
});

export default function RegisterScreen({navigation}:any){
  const { signUp, loading } = React.useContext(AuthContext) as any;
  return (
    <View style={{ padding:16, gap:8 }}>
      <Text variant="headlineMedium">Criar conta</Text>
      <Formik
        initialValues={{name:'', email:'', password:''}}
        validationSchema={schema}
        onSubmit={async (v,{setStatus})=>{
          try { await signUp(v.name, v.email, v.password); }
          catch (e:any){ setStatus(e?.response?.data?.message || 'Erro no cadastro'); }
        }}>
        {({handleChange, handleBlur, handleSubmit, values, errors, touched, status})=>(
          <>
            <TextInput label="Nome" value={values.name} onChangeText={handleChange('name')} onBlur={handleBlur('name')} />
            <HelperText type="error" visible={touched.name && !!errors.name}>{errors.name}</HelperText>
            <TextInput label="E-mail" value={values.email} onChangeText={handleChange('email')} onBlur={handleBlur('email')} keyboardType="email-address" autoCapitalize="none" />
            <HelperText type="error" visible={touched.email && !!errors.email}>{errors.email}</HelperText>
            <TextInput label="Senha" value={values.password} onChangeText={handleChange('password')} onBlur={handleBlur('password')} secureTextEntry />
            <HelperText type="error" visible={touched.password && !!errors.password}>{errors.password}</HelperText>
            {status ? <HelperText type="error" visible>{status}</HelperText> : null}
            {loading ? <ActivityIndicator /> : <Button mode="contained" onPress={handleSubmit as any}>Cadastrar</Button>}
            <Button onPress={()=>navigation.goBack()}>Voltar</Button>
          </>
        )}
      </Formik>
    </View>
  );
}
