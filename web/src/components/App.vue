 <template>
   <div class="container">
    <nav class="navbar navbar-default">
      <div>
        <ul class="nav navbar-nav">
          <li><router-link to="home">Home</router-link></li>
          <li><router-link to="edit">Edit Item</router-link></li>
        </ul>
      </div>    
    </nav>
    <div>
      <router-view></router-view>
    </div>
   </div>
  </template>