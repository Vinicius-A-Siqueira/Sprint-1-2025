import React from 'react';
import { View } from 'react-native';
import { Button, TextInput, Switch, HelperText, ActivityIndicator } from 'react-native-paper';
import { Formik } from 'formik';
import * as Yup from 'yup';
import { TaskService } from '../../services/taskService';

const schema = Yup.object({ title: Yup.string().min(3,'Mín. 3').required('Obrigatório') });

export default function TaskFormScreen({route, navigation}:any){
  const id = route?.params?.id as string | undefined;
  const [initial, setInitial] = React.useState({ title:'', done:false });
  const [loading, setLoading] = React.useState(!!id);

  React.useEffect(()=>{
    (async ()=>{
      if (!id) return;
      const t = await TaskService.get(id);
      setInitial({ title: t.title, done: t.done });
      setLoading(false);
    })();
  },[id]);

  if (loading) return <ActivityIndicator style={{ marginTop: 32 }} />;

  return (
    <View style={{ padding:16 }}>
      <Formik
        initialValues={initial}
        enableReinitialize
        validationSchema={schema}
        onSubmit={async (v,{setStatus})=>{
          try{
            if (id) await TaskService.update(id, v);
            else await TaskService.create(v);
            navigation.goBack();
          }catch(e:any){ setStatus(e?.response?.data?.message || 'Erro ao salvar'); }
        }}>
        {({handleChange, handleBlur, handleSubmit, values, errors, touched, status, setFieldValue})=>(
          <>
            <TextInput
              label="Título"
              value={values.title}
              onChangeText={handleChange('title')}
              onBlur={handleBlur('title')}
            />
            <HelperText type="error" visible={touched.title && !!errors.title}>{errors.title}</HelperText>
            <Switch value={values.done} onValueChange={(v)=>setFieldValue('done', v)} />
            {status ? <HelperText type="error" visible>{status}</HelperText> : null}
            <Button mode="contained" onPress={handleSubmit as any}>Salvar</Button>
          </>
        )}
      </Formik>
    </View>
  );
}
