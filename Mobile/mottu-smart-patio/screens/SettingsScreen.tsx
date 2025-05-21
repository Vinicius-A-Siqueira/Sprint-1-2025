import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { Picker } from '@react-native-picker/picker';
import AsyncStorage from '@react-native-async-storage/async-storage';

export default function SettingsScreen() {
  const [visualizacao, setVisualizacao] = useState<string>('lista');

  useEffect(() => {
    // Carregar preferências salvas
    AsyncStorage.getItem('@modo_visualizacao').then(valor => {
      if (valor) setVisualizacao(valor);
    });
  }, []);

  const handleChange = async (valor: string) => {
    setVisualizacao(valor);
    await AsyncStorage.setItem('@modo_visualizacao', valor);
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Configurações</Text>

      <Text style={styles.label}>Modo de visualização das motos:</Text>
      <Picker
        selectedValue={visualizacao}
        onValueChange={handleChange}
        style={styles.picker}
        dropdownIconColor="#0f0"
      >
        <Picker.Item label="Lista" value="lista" />
        <Picker.Item label="Grade" value="grade" />
        <Picker.Item label="Mapa (em breve)" value="mapa" enabled={false} />
      </Picker>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#000',
    padding: 20,
  },
  title: {
    fontSize: 22,
    fontWeight: 'bold',
    color: '#0f0',
    marginBottom: 20,
  },
  label: {
    fontSize: 16,
    color: '#0f0',
    marginBottom: 10,
  },
  picker: {
    backgroundColor: '#111',
    color: '#0f0',
    borderRadius: 5,
  },
});
