<template>
    <div class="col-sm-6 col-sm-offset-3">
      <h1>Auction House</h1>
      <table class="table">
    <tr v-for="material in materials" :key="material.Id">
        <td>{{ material.id }}</td>
        <td>{{ material.name }}</td>
        <td>{{ material.price }}</td>
    </tr>
  </div>
    </div>
  </template>

  <script>
  export default {
    data() {
      
      return {
        quote: '',
        materials: [
          {}
        ]
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
            console.log('loaded new materials')
            console.dir(data)
          })
          .error((err) => console.log(err))
      }
    }
  }
  </script>