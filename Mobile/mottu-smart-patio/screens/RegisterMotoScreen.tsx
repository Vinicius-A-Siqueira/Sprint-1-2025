import React, { useState, useEffect } from 'react';
import { Button, Image, StyleSheet, Text, TextInput, TouchableOpacity, View } from 'react-native';
import * as ImagePicker from 'expo-image-picker';

import { useNavigation, useRoute, CompositeNavigationProp, RouteProp } from '@react-navigation/native';
import type { NativeStackNavigationProp } from '@react-navigation/native-stack';
import type { BottomTabNavigationProp } from '@react-navigation/bottom-tabs';

import type { RootStackParamList, RootTabParamList, Moto } from '../types/types';
import { saveMoto, updateMoto } from '../utils/storage';

type StackNavProp = NativeStackNavigationProp<RootStackParamList>;
type TabNavProp = BottomTabNavigationProp<RootTabParamList>;
type PropsNavigation = CompositeNavigationProp<StackNavProp, TabNavProp>;

type PropsRoute = RouteProp<RootTabParamList, 'Cadastrar Moto'>;

export default function RegisterMotoScreen() {
  const navigation = useNavigation<PropsNavigation>();
  const route = useRoute<PropsRoute>();

  const motoToEdit = route.params?.motoToEdit;

  const [placa, setPlaca] = useState(motoToEdit?.placa ?? '');
  const [status, setStatus] = useState(motoToEdit?.status ?? '');
  const [image, setImage] = useState<string | null>(motoToEdit?.imagem ?? null);

  useEffect(() => {
    if (motoToEdit) {
      setPlaca(motoToEdit.placa);
      setStatus(motoToEdit.status);
      setImage(motoToEdit.imagem);
    }
  }, [motoToEdit]);

  const pickImage = async () => {
    const result = await ImagePicker.launchImageLibraryAsync({
      quality: 1,
      base64: false,
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
    });

    if (!result.canceled) {
      setImage(result.assets[0].uri);
    }
  };

  const gerarIdUnico = () => {
    return Date.now().toString() + Math.random().toString(36).substr(2, 5);
  };

  const salvar = async () => {
    if (placa && status && image) {
      if (motoToEdit) {
        // Atualiza moto existente
        await updateMoto({ id: motoToEdit.id, placa, status, imagem: image });
      } else {
        // Cria nova moto com ID único
        const novoId = gerarIdUnico();
        await saveMoto({ id: novoId, placa, status, imagem: image });
      }
      navigation.navigate('Tabs', { screen: 'Início' });
    } else {
      alert('Por favor, preencha todos os campos e selecione uma imagem.');
    }
  };

  return (
    <View style={styles.container}>
      <Text style={styles.label}>Placa:</Text>
      <TextInput style={styles.input} value={placa} onChangeText={setPlaca} />

      <Text style={styles.label}>Status:</Text>
      <TextInput style={styles.input} value={status} onChangeText={setStatus} />

      <TouchableOpacity onPress={pickImage} style={styles.button}>
        <Text style={styles.buttonText}>Selecionar Imagem</Text>
      </TouchableOpacity>

      {image && <Image source={{ uri: image }} style={styles.preview} />}

      <Button title={motoToEdit ? 'Atualizar Moto' : 'Salvar Moto'} onPress={salvar} color="#0f0" />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#000',
    padding: 20,
  },
  label: {
    fontWeight: 'bold',
    marginBottom: 5,
    color: '#0f0',
  },
  input: {
    borderWidth: 1,
    borderColor: '#0f0',
    padding: 10,
    marginBottom: 15,
    borderRadius: 5,
    color: '#0f0',
  },
  button: {
    backgroundColor: '#000',
    padding: 12,
    borderRadius: 5,
    marginBottom: 15,
    borderWidth: 1,
    borderColor: '#0f0',
  },
  buttonText: {
    color: '#0f0',
    textAlign: 'center',
    fontWeight: 'bold',
    fontSize: 16,
  },
  preview: {
    width: '100%',
    height: 200,
    marginBottom: 20,
    resizeMode: 'contain',
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#0f0',
  },
});
