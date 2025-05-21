import React from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import type { RootStackParamList } from '../types/types';

import Tabs from './tabs';
import MotoDetailsScreen from '../screens/MotoDetailsScreen';
import ListaMotosScreen from '../screens/MotoListScreen';

const Stack = createNativeStackNavigator<RootStackParamList>();

export default function StackNavigator() {
  return (
    <Stack.Navigator>
      <Stack.Screen name="Tabs" component={Tabs} options={{ headerShown: false }} />
      <Stack.Screen name="Detalhes da Moto" component={MotoDetailsScreen} />
      <Stack.Screen name="Lista de Motos" component={ListaMotosScreen} />
    </Stack.Navigator>
  );
}
