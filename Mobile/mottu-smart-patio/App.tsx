import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import React from 'react';

import HomeScreen from './screens/HomeScreen';
import MotoDetailsScreen from './screens/MotoDetailsScreen';
import PatioMapScreen from './screens/PatioMapScreen';
import RegisterMotoScreen from './screens/RegisterMotoScreen';
import SettingsScreen from './screens/SettingsScreen';

export type RootStackParamList = {
  Início: undefined;
  'Mapa do Pátio': undefined;
  'Cadastrar Moto': undefined;
  'Detalhes da Moto': undefined;
  'Configurações': undefined;
};

const Stack = createNativeStackNavigator<RootStackParamList>();

export default function App() {
  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="Início">
        <Stack.Screen name="Início" component={HomeScreen} />
        <Stack.Screen name="Mapa do Pátio" component={PatioMapScreen} />
        <Stack.Screen name="Cadastrar Moto" component={RegisterMotoScreen} />
        <Stack.Screen name="Detalhes da Moto" component={MotoDetailsScreen} />
        <Stack.Screen name="Configurações" component={SettingsScreen} />
      </Stack.Navigator>
    </NavigationContainer>
  );
}
