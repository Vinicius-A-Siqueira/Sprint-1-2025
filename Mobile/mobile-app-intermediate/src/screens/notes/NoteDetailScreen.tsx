import React from 'react';
import { View } from 'react-native';
import { Button, Text, ActivityIndicator } from 'react-native-paper';
import { NoteService } from '../../services/noteService';

export default function NoteDetailScreen({route, navigation}:any){
  const id = route.params.id as string;
  const [note, setNote] = React.useState<any>(null);
  const [loading, setLoading] = React.useState(true);

  React.useEffect(()=>{ (async ()=>{
    setNote(await NoteService.get(id));
    setLoading(false);
  })(); },[id]);

  if (loading) return <ActivityIndicator style={{ marginTop: 32 }} />;

  return (
    <View style={{ padding:16, gap:8 }}>
      <Text variant="headlineSmall">{note.title}</Text>
      <Text>{note.content}</Text>
      <Button mode="outlined" onPress={()=>navigation.navigate('NoteForm',{id})}>Editar</Button>
      <Button mode="contained" onPress={async ()=>{
        await NoteService.remove(id);
        navigation.goBack();
      }}>Excluir</Button>
    </View>
  );
}
