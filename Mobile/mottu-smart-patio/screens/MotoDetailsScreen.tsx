import React from 'react';
import { StyleSheet, Text, View } from 'react-native';

export default function MotoDetailsScreen() {
    return (
        <View style={styles.container}>
            <Text style={styles.title}>🔍 Detalhes da Moto</Text>
            <Text>Placa: ABC1234</Text>
            <Text>Modelo: Mottu Sport 110i</Text>
            <Text>Status: Disponível</Text>
            <Text>Última localização: Pátio SP-03</Text>
        </View>
    );
}

const styles = StyleSheet.create({
    container: { flex: 1, padding: 20 },
    title: { fontSize: 22, marginBottom: 10 }
});
