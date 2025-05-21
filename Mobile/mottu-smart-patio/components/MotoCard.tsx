import React from 'react';
import { View, Text, Image, StyleSheet, TouchableOpacity } from 'react-native';
import { Moto } from '../types/types';

interface Props {
  moto: Moto;
  onPress: () => void;
}

export default function MotoCard({ moto, onPress }: Props) {
  return (
    <TouchableOpacity style={styles.card} onPress={onPress}>
      <Image source={{ uri: moto.imagem }} style={styles.image} />
      <View>
        <Text style={styles.placa}>{moto.placa}</Text>
        <Text style={styles.status}>{moto.status}</Text>
      </View>
    </TouchableOpacity>
  );
}

const styles = StyleSheet.create({
  card: {
    backgroundColor: '#0f0',
    borderRadius: 10,
    padding: 10,
    marginBottom: 10,
    flexDirection: 'row',
    alignItems: 'center',
  },
  image: {
    width: 60,
    height: 40,
    marginRight: 10,
  },
  placa: {
    fontSize: 16,
    fontWeight: 'bold',
  },
  status: {
    fontSize: 14,
  },
});
