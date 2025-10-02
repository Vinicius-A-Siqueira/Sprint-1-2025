import { createNativeStackNavigator } from '@react-navigation/native-stack';
import HomeScreen from '../screens/HomeScreen';
import TaskListScreen from '../screens/tasks/TaskListScreen';
import TaskFormScreen from '../screens/tasks/TaskFormScreen';
import TaskDetailScreen from '../screens/tasks/TaskDetailScreen';
import NoteListScreen from '../screens/notes/NoteListScreen';
import NoteFormScreen from '../screens/notes/NoteFormScreen';
import NoteDetailScreen from '../screens/notes/NoteDetailScreen';

const Stack = createNativeStackNavigator();
export default function AppRoutes(){
  return (
    <Stack.Navigator>
      <Stack.Screen name="Home" component={HomeScreen}/>
      <Stack.Screen name="TaskList" component={TaskListScreen}/>
      <Stack.Screen name="TaskForm" component={TaskFormScreen}/>
      <Stack.Screen name="TaskDetail" component={TaskDetailScreen}/>
      <Stack.Screen name="NoteList" component={NoteListScreen}/>
      <Stack.Screen name="NoteForm" component={NoteFormScreen}/>
      <Stack.Screen name="NoteDetail" component={NoteDetailScreen}/>
    </Stack.Navigator>
  );
}
