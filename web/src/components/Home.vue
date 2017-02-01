<template>
    <div class="col-sm-10 col-sm-offset-1">
      <h1>Auction House</h1>
      <table class="table">
        <tr>
          <th>Id</th>
          <th>Status</th>
          <th v-on:click=sort('name')>Name</th>
          <th v-on:click=sort('price')>Price</th>
          <th v-on:click=sort('craft')>Craft Cost</th>
          <th v-on:click=sort('profit')>Profit</th>
          <th v-on:click=sort('fee')>Fee</th>
          <th>Low</th>
          <th>High</th>
          <th v-on:click=sort('labor')>Labor</th>
          <th v-on:click=sort('ppl')>Profit/Labor</th>
          <th v-on:click=sort('margin')>Margin</th>
        </tr>
    <tr v-for="material in materials" :key="material.Id">
        <td>{{ material.id }}</td>
        <td>{{ material.status }}</td>
        <td>{{ material.name }}</td>
        <td>{{ material.price }}</td>
        <td>{{ material.materialcost }}</td>
        <td>{{ material.profit }}</td>
        <td>{{ material.fee }}</td>
        <td>{{ material.low}}</td>
        <td>{{ material.high}}</td>
        <td>{{ material.labor}}</td>
        <td>{{ material.profitPerLabor }}</td>
        <td>{{ material.margin }}</td>
    </tr>
  </div>
    </div>
  </template>

  <script>
  import auth from '../auth'
  export default {
    data() {
      
      return {
        quote: '',
        materials: [],
        ascending: false
      }
    },
    created(){
      this.getMaterials()
    },
    methods: {
      getMaterials() {
        this.$http
          .get('http://localhost:3333/material/list', (data) => {
            this.materials = data;
          })
          .error((err) => console.log(err))
      },
      sort(col){
        switch(col){
          case 'name':
            if(this.ascending)
              this.materials.sort((a,b) => a.name.localeCompare(b.name))
            else
               this.materials.sort((a,b) => b.name.localeCompare(a.name))
          break
          case 'price':
            if(this.ascending)
              this.materials.sort((a,b) => a.price - b.price)
            else
              this.materials.sort((a,b) => b.price - a.price)
            break
          case 'profit':
            if(this.ascending)
              this.materials.sort((a,b) => a.profit - b.profit)
            else
              this.materials.sort((a,b) => b.profit - a.profit)
            break
          case 'craft':
            if(this.ascending)
              this.materials.sort((a,b) => a.materialcost - b.materialcost)
            else
              this.materials.sort((a,b) => b.materialcost - a.materialcost)
            break
          case 'ppl':
            if(this.ascending)
              this.materials.sort((a,b) => a.profitperlabor - b.profitperlabor)
            else
              this.materials.sort((a,b) => b.profitperlabor - a.profitperlabor)
            break       
          case 'labor':
            if(this.ascending)
              this.materials.sort((a,b) => a.labor - b.labor)
            else
              this.materials.sort((a,b) => b.labor - a.labor)
            break                               
          }
          this.ascending = !this.ascending
      }
    },
        route: {
      // Check the users auth status before
      // allowing navigation to the route
      canActivate() {
        return auth.user.authenticated
      }
    }
  }
  </script>