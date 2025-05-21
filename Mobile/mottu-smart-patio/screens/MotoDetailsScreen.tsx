import { RouteProp, useNavigation } from '@react-navigation/native';
import React from 'react';
import { View, Text, Image, StyleSheet, Button, Alert } from 'react-native';
import { RootStackParamList, Moto } from '../types/types';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { deleteMoto } from '../utils/storage';

type Props = {
  route: RouteProp<RootStackParamList, 'Detalhes da Moto'>;
};

type NavigationProp = NativeStackNavigationProp<RootStackParamList>;

export default function MotoDetailsScreen({ route }: Props) {
  const { moto } = route.params;
  const navigation = useNavigation<NavigationProp>();

  const handleDelete = () => {
    Alert.alert(
      'Confirmar exclusão',
      `Deseja realmente excluir a moto ${moto.placa}?`,
      [
        { text: 'Cancelar', style: 'cancel' },
        { 
          text: 'Excluir', 
          style: 'destructive', 
          onPress: async () => {
            await deleteMoto(moto.id);
            navigation.navigate('Lista de Motos');
          } 
        },
      ]
    );
  };

  const handleEdit = () => {
    navigation.navigate('Tabs', {
      screen: 'Cadastrar Moto',
      params: { motoToEdit: moto },
    });
  };

  return (
    <View style={styles.container}>
      <Image source={{ uri: moto.imagem }} style={styles.image} />
      <Text style={styles.label}>Placa:</Text>
      <Text style={styles.text}>{moto.placa}</Text>
      <Text style={styles.label}>Status:</Text>
      <Text style={styles.text}>{moto.status}</Text>

      <View style={styles.buttons}>
        <Button title="Editar" onPress={handleEdit} color="#0a0" />
        <Button title="Deletar" onPress={handleDelete} color="#a00" />
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#000',
    padding: 20,
    alignItems: 'center',
  },
  image: {
    width: 200,
    height: 150,
    resizeMode: 'contain',
    marginBottom: 20,
  },
  label: {
    fontSize: 18,
    color: '#0f0',
    fontWeight: 'bold',
  },
  text: {
    fontSize: 16,
    color: '#fff',
    marginBottom: 20,
  },
  buttons: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    width: '60%',
  },
});
