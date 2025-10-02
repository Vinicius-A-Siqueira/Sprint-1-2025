import React, { useState, useEffect } from 'react';
import { View, FlatList, StyleSheet, Text } from 'react-native';
import MotoCard from '../components/MotoCard';
import { useIsFocused } from '@react-navigation/native';

type Moto = {
  id: string;
  placa: string;
  status: string;
  imagemUri?: string | null;
};

export default function HomeScreen() {
  const isFocused = useIsFocused();
  const [motos, setMotos] = useState<Moto[]>([]);

  // Aqui usamos AsyncStorage apenas como exemplo local; adapte para API real
  useEffect(() => {
    // carrega motos do storage local
    (async () => {
      try {
        const raw = await (await import('@react-native-async-storage/async-storage')).default.getItem('motos');
        if (raw) setMotos(JSON.parse(raw));
      } catch (e) {
        console.warn('Erro ao carregar motos', e);
      }
    })();
  }, [isFocused]);

  if (!motos.length) {
    return (
      <View style={styles.empty}>
        <Text>Nenhuma moto cadastrada ainda. Use Cadastrar no menu acima.</Text>
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <FlatList
        data={motos}
        keyExtractor={(item) => item.id}
        renderItem={({ item }) => <MotoCard {...item} />}
        contentContainerStyle={{ padding: 12 }}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1 },
  empty: { flex: 1, justifyContent: 'center', alignItems: 'center', padding: 24 },
});
