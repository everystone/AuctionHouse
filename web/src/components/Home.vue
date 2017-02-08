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

    <div class="col-sm-11 listholder" v-bind:style="listStyle">
      <table class="table table-striped table-hover productlist" style="table-layout:fixed">
        <tbody>
    <tr v-for="item in items" :key="item.Id" v-on:click="quickEdit(item)">
        <td width="3">{{ item.id }}</td>
        <td width="3">{{ item.status }}</td>
        <td width="50">{{ item.name }}</td>
        <td width="5">{{ item.price }}</td>
        <td width="5">{{ item.materialCost }}</td>
        <td width="5">{{ item.profit }}</td>
        <td width="5">{{ item.fee }}</td>
        <td width="5">{{ item.low}}</td>
        <td width="5">{{ item.high}}</td>
        <td width="5">{{ item.labor}}</td>
        <td width="5">{{ item.profitPerLabor }}</td>
        <td width="5">{{ item.margin }}</td>
        <td width="5"><Button class="btn btn-sm btn-info" @click="edit(item)">edit</Button></td>
    </tr>
    </tbody>
      </table>
    </div>
  </template>

  <script>
  import Modal from './Modal'
  export default {
    data() {
      return {
        quote: '',
        ascending: false,
        search: '',
        showModal: false,
        quickEditMaterial: {},
        listStyle: {
          'height': '350px',
          'overflow-y': 'auto'
        }
      }
    },
    components: {
      'modal': Modal
    },
    computed: {
      items: function() {
        return this.$store.state.filtered
      }
    },
    mounted() {
      // set size of table
      this.listStyle.height = (window.innerHeight - 200) + 'px'
    },
    created() {
      this.$store.dispatch('fetch', this)
    },
    watch: {
      search: function(val, old) {
        this.$store.dispatch('filter', val)
      }
    },
    methods: {
      quickEdit(mat){
        this.quickEditMaterial = mat
        this.showModal = true
      },
      edit(mat){
        // api.navigate(this, 'material')
        this.$store.dispatch('load', mat)
        this.$router.push('edit')
      },
      closeModal() {
        this.showModal = false
        // highlight the changed items in list somehow.. too see which items was affected by the price change.
      },
      sort(col){
        switch (col){
          case 'name':
            if (this.ascending) {
              this.items.sort((a, b) => a.name.localeCompare(b.name))
            } else {
              this.items.sort((a, b) => b.name.localeCompare(a.name))
            }
            break
          default:
            if (this.ascending) {
              this.items.sort((a, b) => a[col] - b[col])
            } else {
              this.items.sort((a, b) => b[col] - a[col])
            }
            break
        }
        this.ascending = !this.ascending
      }
    }
  }
  </script>