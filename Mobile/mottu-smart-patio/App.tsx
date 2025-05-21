import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import StackNavigator from './navigation/stack';

export default function App() {
  return (
    <NavigationContainer>
      <StackNavigator />
    </NavigationContainer>
  );
}
