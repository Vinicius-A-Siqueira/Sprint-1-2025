import React from 'react';
import { StyleSheet, Text, View } from 'react-native';

export default function PatioMapScreen() {
    return (
        <View style={styles.container}>
            <Text style={styles.title}>📍 Mapa do Pátio</Text>
            <Text>Motos posicionadas em tempo real (simulado)</Text>
            <View style={styles.mapMock}>
                <Text>[🏍️][ ] [ ] [🏍️]</Text>
                <Text>[ ]  [🏍️] [ ]</Text>
            </View>
        </View>
    );
}

const styles = StyleSheet.create({
    container: { flex: 1, alignItems: 'center', justifyContent: 'center' },
    title: { fontSize: 22, marginBottom: 10 },
    mapMock: { marginTop: 20, padding: 20, borderWidth: 1, borderColor: '#aaa' }
});
