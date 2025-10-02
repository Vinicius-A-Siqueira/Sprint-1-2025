import React from 'react';
import { View, Image, Text, StyleSheet, TouchableOpacity } from 'react-native';
import { useNavigation } from '@react-navigation/native';

export default function TopNav() {
  const nav = useNavigation();

  return (
    <View style={styles.container}>
      <Image
        source={require('../../assets/logo-mottu.png')}
        style={styles.logo}
        resizeMode="contain"
      />
      <View style={styles.actions}>
        <TouchableOpacity onPress={() => nav.navigate('Home')}>
          <Text style={styles.link}>Home</Text>
        </TouchableOpacity>
        <TouchableOpacity onPress={() => nav.navigate('RegisterMoto')}>
          <Text style={styles.link}>Cadastrar</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    height: 64,
    paddingHorizontal: 12,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    borderBottomWidth: 1,
    borderBottomColor: '#e6e6e6',
    backgroundColor: '#fff',
  },
  logo: { width: 140, height: 40 },
  actions: { flexDirection: 'row', gap: 12 },
  link: { marginHorizontal: 8, fontSize: 16, color: '#333' },
});
