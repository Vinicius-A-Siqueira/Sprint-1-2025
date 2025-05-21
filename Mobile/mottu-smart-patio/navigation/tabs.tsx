import React from 'react';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import MotoListScreen from '../screens/MotoListScreen';
import RegisterMotoScreen from '../screens/RegisterMotoScreen';
import ConfigScreen from '../screens/SettingsScreen';
import HomeScreen from '../screens/HomeScreen';
import { RootTabParamList } from '../types/types';

const Tab = createBottomTabNavigator<RootTabParamList>();

export default function TabsNavigator() {
  return (
    <Tab.Navigator
      screenOptions={{
        headerShown: false,
        tabBarActiveTintColor: '#0f0',
        tabBarStyle: { backgroundColor: '#000' },
      }}
    >
      <Tab.Screen name="Início" component={HomeScreen} />
      <Tab.Screen name="Lista de Motos" component={MotoListScreen} />
      <Tab.Screen name="Cadastrar Moto" component={RegisterMotoScreen} />
      <Tab.Screen name="Configurações" component={ConfigScreen} />
    </Tab.Navigator>
  );
}
