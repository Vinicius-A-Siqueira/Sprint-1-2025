import React from 'react';
import { View, Text, Image, StyleSheet } from 'react-native';

export default function HomeScreen() {
  return (
    <View style={styles.container}>
      <Image source={require('../assets/logo-mottu.png')} style={styles.logo} />
      <Text style={styles.description}>
        Bem-vindo ao Mottu Smart Pátio! Este app permite cadastrar, listar e visualizar detalhes das motos, além de mapear o pátio de forma inteligente.
      </Text>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#000',
    alignItems: 'center',
    justifyContent: 'center',
    padding: 20,
  },
  logo: {
    width: 200,
    height: 200,
    resizeMode: 'contain',
    marginBottom: 30,
  },
  description: {
    color: '#0f0',
    fontSize: 18,
    textAlign: 'center',
  },
});
