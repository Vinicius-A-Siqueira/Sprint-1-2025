import React, { useEffect, useState } from 'react';
import { View, Text, StyleSheet, FlatList } from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { Moto } from '../types/types';
import MotoCard from '../components/MotoCard';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { RootStackParamList } from '../types/types';

export default function MotoListScreen() {
  const [motos, setMotos] = useState<Moto[]>([]);
  const [modoVisualizacao, setModoVisualizacao] = useState<'lista' | 'grade'>('lista');

  const navigation = useNavigation<NativeStackNavigationProp<RootStackParamList>>();

  useEffect(() => {
    async function loadMotos() {
      const data = await AsyncStorage.getItem('@motos');
      if (data) {
        setMotos(JSON.parse(data));
      }

      const modo = await AsyncStorage.getItem('@modo_visualizacao');
      if (modo === 'grade') setModoVisualizacao('grade');
      else setModoVisualizacao('lista');
    }

    loadMotos();
  }, []);

  const renderMoto = ({ item }: { item: Moto }) => (
    <MotoCard
      moto={item}
      onPress={() => navigation.navigate('Detalhes da Moto', { moto: item })}
    />
  );

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Lista de Motos</Text>
      <FlatList
        data={motos}
        key={modoVisualizacao === 'grade' ? 'g' : 'l'}
        keyExtractor={item => item.id}
        renderItem={renderMoto}
        numColumns={modoVisualizacao === 'grade' ? 2 : 1}
        columnWrapperStyle={modoVisualizacao === 'grade' ? styles.column : undefined}
        contentContainerStyle={{ paddingBottom: 20 }}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#000',
    padding: 10,
  },
  title: {
    color: '#0f0',
    fontSize: 20,
    fontWeight: 'bold',
    marginBottom: 10,
  },
  column: {
    justifyContent: 'space-between',
  },
});
