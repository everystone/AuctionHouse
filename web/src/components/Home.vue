<template>
    <div class="col-sm-10">
      <h1>Auction House</h1>
      <div class="form-group">
      <input type="text" v-model="search" class="form-control" placeholder="Search">
    </div>
      <table class="table table-striped table-hover">
        <thead class="thead-inverse">
        <tr>
          <th>#</th>
          <th>Status</th>
          <th v-on:click=sort('name')>Name</th>
          <th v-on:click=sort('price')>Price</th>
          <th v-on:click=sort('materialCost')>Craft</th>
          <th v-on:click=sort('profit')>Profit</th>
          <th v-on:click=sort('fee')>Fee</th>
          <th v-on:click=sort('low')>Low</th>
          <th v-on:click=sort('high')>High</th>
          <th v-on:click=sort('labor')>Labor</th>
          <th v-on:click=sort('profitPerlabor')>Profit/Labor</th>
          <th v-on:click=sort('margin')>Margin</th>
        </tr>
        </thead>
        <tbody>
    <tr v-for="material in materials" :key="material.Id" v-on:click="edit(material)">
        <td>{{ material.id }}</td>
        <td>{{ material.status }}</td>
        <td>{{ material.name }}</td>
        <td>{{ material.price }}</td>
        <td>{{ material.materialCost }}</td>
        <td>{{ material.profit }}</td>
        <td>{{ material.fee }}</td>
        <td>{{ material.low}}</td>
        <td>{{ material.high}}</td>
        <td>{{ material.labor}}</td>
        <td>{{ material.profitPerLabor }}</td>
        <td>{{ material.margin }}</td>
    </tr>
    </tbody>
      </table>
    </div>
    <div class="col-sm-2">
      <p>Graphs.</p>
    </div>
  </template>

  <script>
  import api from '../api'
  import store from '../store'
  export default {
    data() {
      
      return {
        quote: '',
        materials: [],
        ascending: false,
        search: ''
      }
    },
    created(){
      api.getMaterials(this, () => {
        this.materials = store.state.list
        this.materials.sort((a,b) => b.profit - a.profit)
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
      sort(col){
        switch(col){
          case 'name':
            if(this.ascending)
              this.materials.sort((a,b) => a.name.localeCompare(b.name))
            else
               this.materials.sort((a,b) => b.name.localeCompare(a.name))
          break
          default:
          if(this.ascending)
            this.materials.sort((a,b) => a[col] - b[col])
          else
            this.materials.sort((a,b) => b[col] - a[col])
          break                           
          }
          this.ascending = !this.ascending
      }
    },
        route: {
      // Check the users auth status before
      // allowing navigation to the route
      canActivate() {
        return api.user.authenticated
      }
    }
  }
  </script>