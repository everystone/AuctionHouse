<template>
  <div class="col-sm-4 col-sm-offset-4">
    <h2>{{ material.name }}</h2>
    <div class="alert alert-danger" v-if="error">
      <p>{{ error }}</p>
    </div>
    <div class="form-group">
      <input type="text" class="form-control" placeholder="Name" v-model="material.name">
    </div>
    <div class="form-group">
      <input type="number" v-model.number="material.price" class="form-control" placeholder="Price">
      <input type="number" v-model.number="material.labor" class="form-control" placeholder="Labor">
      <input type="number" v-model.number="material.produce" class="form-control" placeholder="Produce">
    </div>

     <div class="form-group">
      <select v-model="selected" class="form-control">
        <option v-for="material in mats" v-bind:value="material.id">
        {{ material.name}}
        </option>
        </select>
        <input type="number" v-model.number="count" class="form-control" placeholder="Count">
        <button class="btn btn-primary" @click="add()" class="form-control">Add</button>
     </div>

    <table class="table">
      <thead>
        <tr>
          <th>Material</th>
          <th>Count</th>
        </tr>
      </thead>
      <tbody>
      <tr v-for="info in material.craftingRecipe">
        <td>{{ info.id }}</td>
        <td>{{ info.count }}</td>
      </tr>
      </tbody>
    </table>

    <button class="btn btn-primary" @click="save()">Save</button>
  </div>
</template>

  <script>
  import api from '../api'
  import store from '../store'
  export default {
    data() {
      return {
        material:{
          name: '',
          price: 0,
          labor: 0,
          experience: 0,
          produce: 1,
          id: 0
        },
        mats : store.state.list,
        selected: {},
        count: 0
      }
    },

    methods: {
      save() {
        const mat = {
          Id: this.material.id,
          Name: this.material.name,
          Price: this.material.price,
          Labor: this.material.labor,
          Produce: this.material.produce,
          craftingRecipe: this.material.craftingRecipe
        }
        api.saveMaterial(this, mat)
      },
      add(){
        if(this.material.craftingRecipe == null){
          this.material.craftingRecipe = []
        }
        console.log(this.material)
        this.material.craftingRecipe.push({
          'id': this.selected,
          'count':  this.count
        })
      }
    },
    created(){
      this.material = store.state.selected
      console.log('Loaded:')
      console.log(this.material)
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