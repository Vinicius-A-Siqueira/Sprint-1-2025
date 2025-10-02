import React, { useState } from 'react';
import { View, Text, TextInput, Button, StyleSheet, Image, Alert } from 'react-native';
import * as ImagePicker from 'expo-image-picker';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { v4 as uuidv4 } from 'uuid';

export default function RegisterMoto({ navigation }: any) {
  const [placa, setPlaca] = useState('');
  const [status, setStatus] = useState('Disponível');
  const [imageUri, setImageUri] = useState<string | null>(null);

  const pickImage = async () => {
    const permission = await ImagePicker.requestMediaLibraryPermissionsAsync();
    if (!permission.granted) {
      Alert.alert('Permissão necessária', 'Permita acesso às imagens');
      return;
    }
    const result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      quality: 0.6,
    });
    if (!result.cancelled) {
      // result.uri for older sdk or result.assets[0].uri for newer
      const uri = (result as any).uri ?? (result as any).assets?.[0]?.uri;
      setImageUri(uri);
    }
  };

  const takePhoto = async () => {
    const perm = await ImagePicker.requestCameraPermissionsAsync();
    if (!perm.granted) {
      Alert.alert('Permissão câmera', 'Permita acesso à câmera');
      return;
    }
    const result = await ImagePicker.launchCameraAsync({ quality: 0.6 });
    if (!result.cancelled) {
      const uri = (result as any).uri ?? (result as any).assets?.[0]?.uri;
      setImageUri(uri);
    }
  };

  const save = async () => {
    if (!placa.trim()) {
      Alert.alert('Validação', 'Informe a placa');
      return;
    }

    const newMoto = { id: uuidv4(), placa: placa.trim(), status, imagemUri: imageUri ?? null };
    try {
      const raw = await AsyncStorage.getItem('motos');
      const list = raw ? JSON.parse(raw) : [];
      list.unshift(newMoto);
      await AsyncStorage.setItem('motos', JSON.stringify(list));
      Alert.alert('Sucesso', 'Moto cadastrada');
      navigation.navigate('Home');
    } catch (e) {
      console.warn(e);
      Alert.alert('Erro', 'Não foi possível salvar');
    }
  };

  return (
    <View style={styles.container}>
      <Text style={styles.label}>Placa</Text>
      <TextInput style={styles.input} value={placa} onChangeText={setPlaca} placeholder="ABC1D23" />
      <Text style={styles.label}>Status</Text>
      <View style={{ marginBottom: 12 }}>
        <Button title={status} onPress={() => setStatus(status === 'Disponível' ? 'Em manutenção' : 'Disponível')} />
      </View>

      <Text style={styles.label}>Imagem</Text>
      {imageUri ? (
        <Image source={{ uri: imageUri }} style={styles.preview} />
      ) : (
        <View style={[styles.preview, { justifyContent: 'center', alignItems: 'center' }]}>
          <Text>Sem imagem selecionada</Text>
        </View>
      )}
      <View style={styles.row}>
        <Button title="Escolher da galeria" onPress={pickImage} />
        <View style={{ width: 8 }} />
        <Button title="Tirar foto" onPress={takePhoto} />
      </View>

      <View style={{ marginTop: 18 }}>
        <Button title="Salvar" onPress={save} />
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: { padding: 12, flex: 1 },
  label: { fontWeight: '700', marginTop: 8 },
  input: { borderWidth: 1, borderColor: '#ddd', padding: 8, borderRadius: 6, marginTop: 6 },
  preview: { width: '100%', height: 180, borderRadius: 8, backgroundColor: '#fafafa', marginTop: 8 },
  row: { flexDirection: 'row', marginTop: 8 },
});