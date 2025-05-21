import React from 'react';
import { View, Text, StyleSheet } from 'react-native';

export default function PatioMapScreen() {
  return (
    <View style={styles.container}>
      <Text style={styles.text}>Mapa do Pátio (em desenvolvimento)</Text>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#000',
    alignItems: 'center',
    justifyContent: 'center',
  },
  text: {
    color: '#0f0',
    fontSize: 18,
  },
});
