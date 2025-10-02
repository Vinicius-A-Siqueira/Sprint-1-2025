import React from 'react';
import { View } from 'react-native';
import { Button, Text } from 'react-native-paper';
import { AuthContext } from '../contexts/AuthContext';
import ThemeToggle from '../components/ThemeToggle';

export default function HomeScreen({navigation}:any){
  const { signOut, user } = React.useContext(AuthContext) as any;
  return (
    <View style={{ padding:16, gap:12 }}>
      <Text variant="headlineSmall">Olá, {user?.name || 'usuário'} 👋</Text>
      <ThemeToggle />
      <Button mode="contained" onPress={()=>navigation.navigate('TaskList')}>Tasks</Button>
      <Button mode="contained" onPress={()=>navigation.navigate('NoteList')}>Notes</Button>
      <Button mode="outlined" onPress={signOut}>Sair</Button>
    </View>
  );
}
