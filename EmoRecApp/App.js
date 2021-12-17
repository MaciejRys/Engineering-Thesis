/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 * @flow strict-local
 */

import React, { useEffect, useState } from 'react';
import type {Node} from 'react';
import {
  SafeAreaView,
  ScrollView,
  StatusBar,
  StyleSheet,
  Text,
  Button,
  useColorScheme,
  FlatList,
  ActivityIndicator,
  View,
} from 'react-native';

import {
  Colors,
  DebugInstructions,
  Header,
  LearnMoreLinks,
  ReloadInstructions,
} from 'react-native/Libraries/NewAppScreen';

const App: () => Node = () => {
  const isDarkMode = useColorScheme() === 'dark';

  const backgroundStyle = {
    backgroundColor: isDarkMode ? Colors.darker : Colors.lighter,
  };

    const [isLoading, setLoading] = useState(true);
    const [data, setData] = useState([]);

 const getPrediction = async () => {
     try {
        const response = await fetch('http://localhost:58720/predict', {
                                 method: 'POST',
                                 headers: {
                                   Accept: 'application/json',
                                   'Content-Type': 'application/json'
                                 },
                                 body: JSON.stringify({
                                   highGamma: 1,
                                   label: ''
                                 })
                               });
        const json = await response.json();
        console.log(json);
        setData(json.prediction);
    } catch (error) {
      console.error(error);
    } finally {
      setLoading(false);
    }

  }

 useEffect(() => {
getPrediction();
  }, []);

  return (
    <SafeAreaView style={backgroundStyle}>
      <StatusBar barStyle={isDarkMode ? 'light-content' : 'dark-content'} />
      <ScrollView
        contentInsetAdjustmentBehavior="automatic"
        style={backgroundStyle}>
        <Header />
    <View style={{ flex: 1, padding: 24 }}>
    <Text>
    {data}
        </Text>
        </View>
          </ScrollView>
        </SafeAreaView>
      );
    };



export default App;
