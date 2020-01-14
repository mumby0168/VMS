import React from 'react';
import './App.css';
import Layout from './Layout'
import {connect} from 'react-redux'

function App() {
  return <Layout></Layout>
}

export default connect()(App);
