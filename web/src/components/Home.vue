<template>
  <div>
      <div class="col-sm-6">
      <input type="text" v-model="search" class="form-control" placeholder="Search" style="margin-top:15px">
    </div>
    <div class="col-sm-4">
      <!--<chart :height="100" v-bind:chartData="chartData"></chart>-->
    </div>

    <modal v-if="showModal" v-bind:material="quickEditMaterial" @close="closeModal"/>

  <div class="col-sm-11">
    <table class="table table-striped table-hover" style="table-layout:fixed">
      <thead class="thead-inverse">
        <tr>
          <th width="3">#</th>
          <th width="3">Status</th>
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
          <th width="5">Actions</th>

        </tr>
    </thead>
    </table>
    </div>

    <div class="col-sm-11" style="overflow-y: auto;height: 700px;">
      <table class="table table-striped table-hover" style="table-layout:fixed">
        <tbody>
    <tr v-for="material in materials" :key="material.Id" v-on:click="quickEdit(material)">
        <td width="3">{{ material.id }}</td>
        <td width="3">{{ material.status }}</td>
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
        <td width="5"><Button class="btn btn-sm btn-info" @click="edit(material)">edit</Button></td>
    </tr>
    </tbody>
      </table>
    </div>
  </template>

  <script>
  import api from '../api'
  import store from '../store'
  import Modal from './Modal'
  export default {
    data() {
      return {
        quote: '',
        materials: [],
        ascending: false,
        search: '',
        showModal: false,
        quickEditMaterial: {}
      }
    },
    components: {
      'modal': Modal
    },
    created(){
      api.getMaterials(this, () => {
        console.log('Loaded mats.')
        this.materials = store.state.list
        if (this.materials) {
          this.materials.sort((a, b) => b.profit - a.profit)
        }
      })
    },
    watch: {
      search: function(val, old) {
        this.materials = store.state.list.filter(m => m.name.toLowerCase().indexOf(val.toLowerCase()) >= 0)
      }
    },
    methods: {
      quickEdit(mat){
        this.quickEditMaterial = mat
        this.showModal = true
      },
      edit(mat){
        store.setSelected(mat)
        api.navigate('material')
      },
      closeModal() {
        this.showModal = false
        this.materials = store.state.list
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