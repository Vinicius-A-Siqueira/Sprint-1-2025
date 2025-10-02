import React from 'react';
import { View } from 'react-native';
import { Button, TextInput, HelperText, ActivityIndicator } from 'react-native-paper';
import { Formik } from 'formik';
import * as Yup from 'yup';
import { NoteService } from '../../services/noteService';

const schema = Yup.object({ title: Yup.string().min(3,'Mín. 3').required('Obrigatório'), content: Yup.string().min(3,'Mín. 3').required('Obrigatório') });

export default function NoteFormScreen({route, navigation}:any){
  const id = route?.params?.id as string | undefined;
  const [initial, setInitial] = React.useState({ title:'', content:'' });
  const [loading, setLoading] = React.useState(!!id);

  React.useEffect(()=>{
    (async ()=>{
      if (!id) return;
      const n = await NoteService.get(id);
      setInitial({ title: n.title, content: n.content });
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
            if (id) await NoteService.update(id, v);
            else await NoteService.create(v);
            navigation.goBack();
          }catch(e:any){ setStatus(e?.response?.data?.message || 'Erro ao salvar'); }
        }}>
        {({handleChange, handleBlur, handleSubmit, values, errors, touched, status})=>(
          <>
            <TextInput label="Título" value={values.title} onChangeText={handleChange('title')} onBlur={handleBlur('title')} />
            <HelperText type="error" visible={touched.title && !!errors.title}>{errors.title}</HelperText>
            <TextInput label="Conteúdo" value={values.content} onChangeText={handleChange('content')} onBlur={handleBlur('content')} multiline numberOfLines={6} />
            <HelperText type="error" visible={touched.content && !!errors.content}>{errors.content}</HelperText>
            {status ? <HelperText type="error" visible>{status}</HelperText> : null}
            <Button mode="contained" onPress={handleSubmit as any}>Salvar</Button>
          </>
        )}
      </Formik>
    </View>
  );
}
