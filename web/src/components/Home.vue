<template>
  <div>
      <div class="col-sm-6">
      <input type="text" v-model="search" class="form-control" placeholder="Search" style="margin-top:15px">
    </div>
    <div class="col-sm-2">
      <chart :height="250" v-bind:chartData="chartData"></chart>
    </div>
  <div class="col-sm-11">
    <table class="table table-striped table-hover" style="table-layout:fixed">
      <thead>
        <tr>
          <th width="5">#</th>
          <th width="5">#</th>
          <th width="50" v-on:click="sort('name')">Name</th>
          <th width="5" v-on:click="sort('price')">Price</th>
          <th width="5" v-on:click="sort('materialCost')">Craft</th>
          <th width="5" v-on:click="sort('profit')">Profit</th>
          <th width="5" v-on:click="sort('fee')">Fee</th>
          <th width="5" v-on:click="sort('low')">Low</th>
          <th width="5" v-on:click="sort('high')">High</th>
          <th width="5" v-on:click="sort('labor')">Labor</th>
          <th width="5" v-on:click="sort('profitPerlabor')">P/L</th>
          <th width="5" v-on:click="sort('margin')">Margin</th>
        </tr>
    </thead>
    </table>
    </div>

    <div class="col-sm-11" style="overflow-y: auto;height: 700px;">
      <table class="table table-striped table-hover" style="table-layout:fixed">
        <tbody>
    <tr v-for="material in materials" :key="material.Id" v-on:click="edit(material)" v-on:mouseover="loadChart(material)">
        <td width="5">{{ material.id }}</td>
        <td width="5">{{ material.status }}</td>
        <td width="50">{{ material.name }}</td>
        <td width="5">{{ material.price }}</td>
        <td width="5">{{ material.materialCost }}</td>
        <td width="5">{{ material.profit }}</td>
        <td width="5">{{ material.fee }}</td>
        <td width="5">{{ material.low}}</td>
        <td width="5">{{ material.high}}</td>
        <td width="5">{{ material.labor}}</td>
        <td width="5">{{ material.profitPerLabor }}</td>
        <td width="5">{{ material.margin }}</td>
    </tr>
    </tbody>
      </table>
    </div>
  </template>

  <script>
  import api from '../api'
  import store from '../store'
  import Chart from './HistoryChart'
  export default {
    data() {
      return {
        quote: '',
        materials: [],
        ascending: false,
        search: '',
        chartData: []
      }
    },
    components: {
      'chart': Chart
    },
    created(){
      api.getMaterials(this, () => {
        this.materials = store.state.list
        this.materials.sort((a, b) => b.profit - a.profit)
        console.log(this.materials)
      })
    },
    watch: {
      search: function(val, old) {
        this.materials = store.state.list.filter(m => m.name.toLowerCase().indexOf(val.toLowerCase()) >= 0)
      }
    },
    methods: {

      edit(mat){
        store.setSelected(mat)
        api.navigate('material')
      },
      loadChart(material) {
        console.log('load: ' + material)
        this.chartData = {
          labels: material.history.map(h => '.'),
          datasets: [
            {
              label: 'Price History',
              backgroundColor: '#f87979',
              data: material.history.map(h => h.price)
            }
          ]
        }
      },
      sort(col){
        switch (col){
          case 'name':
            if (this.ascending) {
              this.materials.sort((a, b) => a.name.localeCompare(b.name))
            } else {
              this.materials.sort((a, b) => b.name.localeCompare(a.name))
            }
            break
          default:
            if (this.ascending) {
              this.materials.sort((a, b) => a[col] - b[col])
            } else {
              this.materials.sort((a, b) => b[col] - a[col])
            }
            break
        }
        this.ascending = !this.ascending
      }
    }
  }
  </script>