import React from 'react';
import { View, Text, Image, StyleSheet } from 'react-native';

type Props = {
  id: string;
  placa: string;
  status: string;
  imagemUri?: string | null;
};

export default function MotoCard({ placa, status, imagemUri }: Props) {
  return (
    <View style={styles.card}>
      {imagemUri ? (
        <Image source={{ uri: imagemUri }} style={styles.image} />
      ) : (
        <View style={[styles.image, styles.imagePlaceholder]}>
          <Text style={{ color: '#666' }}>Sem imagem</Text>
        </View>
      )}
      <View style={styles.info}>
        <Text style={styles.placa}>{placa}</Text>
        <Text style={styles.status}>Status: {status}</Text>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  card: {
    borderRadius: 8,
    overflow: 'hidden',
    backgroundColor: '#fff',
    marginBottom: 12,
    elevation: 2,
    flexDirection: 'row',
  },
  image: { width: 120, height: 90 },
  imagePlaceholder: { justifyContent: 'center', alignItems: 'center', backgroundColor: '#f1f1f1' },
  info: { padding: 12, flex: 1, justifyContent: 'center' },
  placa: { fontSize: 16, fontWeight: '700' },
  status: { marginTop: 6, color: '#666' },
});
