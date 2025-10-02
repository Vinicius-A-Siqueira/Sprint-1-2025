import React from 'react';
import { View } from 'react-native';
import { Button, Text, ActivityIndicator } from 'react-native-paper';
import { TaskService } from '../../services/taskService';

export default function TaskDetailScreen({route, navigation}:any){
  const id = route.params.id as string;
  const [task, setTask] = React.useState<any>(null);
  const [loading, setLoading] = React.useState(true);

  React.useEffect(()=>{ (async ()=>{
    setTask(await TaskService.get(id));
    setLoading(false);
  })(); },[id]);

  if (loading) return <ActivityIndicator style={{ marginTop: 32 }} />;

  return (
    <View style={{ padding:16, gap:8 }}>
      <Text variant="headlineSmall">{task.title}</Text>
      <Text>{task.done ? 'Concluída' : 'Pendente'}</Text>
      <Button mode="outlined" onPress={()=>navigation.navigate('TaskForm',{id})}>Editar</Button>
      <Button mode="contained" onPress={async ()=>{
        await TaskService.remove(id);
        navigation.goBack();
      }}>Excluir</Button>
    </View>
  );
}
