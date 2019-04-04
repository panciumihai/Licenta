<template>
  <nav>
    <v-toolbar clipped-right app class="toolbar">
      <v-toolbar-side-icon @click="drawer = !drawer" class="blue--text"></v-toolbar-side-icon>

      <router-link to="/" tag="button">
        <v-toolbar-title>
          <v-avatar tile class="mr-1">
            <img src="../assets/Logo_UAIC_Iasi.svg" alt>
          </v-avatar>
          <span class="font-weight-light">Campus</span>
          <span>Management</span>
        </v-toolbar-title>
      </router-link>
      <v-spacer></v-spacer>

      <v-toolbar-items class="hidden-sm-and-down">
        <!-- <v-btn flat color="blue" class="mx-0" v-for="item in navBarItems" :key="item.title">
          <span class="mr-2">{{ item.title }}</span>
        </v-btn>-->

        <v-tabs centered slider-color="blue" height="64px" :value="setTab">
          <v-tab v-show="false" to="/nan"></v-tab>
          <v-tab
            flat
            class="grey lighten-4 blue--text"
            v-for="item in navBarItems"
            :key="item.title"
            :to="item.route"
            router="true"
          >
            <span class="mr-2">{{ item.title }}</span>
          </v-tab>
        </v-tabs>

        <v-btn flat color="blue" class="ml-3 mr-0" @click="dialog = true">
          <span class="mr-2">Autentificare</span>
          <v-icon>meeting_room</v-icon>
          <Login/>
        </v-btn>
        <v-btn depressed dark color="blue" :to="`/register`">
          <span class="mr-2">Inscriere</span>
          <v-icon>exit_to_app</v-icon>
        </v-btn>
      </v-toolbar-items>
    </v-toolbar>
    <v-navigation-drawer app v-model="drawer" class="primary" width="250"></v-navigation-drawer>
    <Login v-model="dialog"></Login>
  </nav>
</template>

<script>
import Login from "@/components/Login";

export default {
  components: {
    Login
  },
  data() {
    return {
      drawer: false,
      dialog: false,
      navBarItems: [
        { title: "Cămine", route: "/camine" },
        { title: "Utile", route: "/utile" },
        { title: "Cantină", route: "/cantina" },
        { title: "Contact", route: "/contact" }
      ]
    };
  },
  methods: {
    openLoginDialog: function() {
      // eslint-disable-next-line
      console.log("click");
      this.$emit("dialog", true);
    }
  },
  computed: {
    setTab: function() {
      var i;
      for (i = 0; i < this.navBarItems.length; ++i) {
        if (this.$route.path == this.navBarItems[i].route)
          return this.$route.path;
      }
      return "/nan";
    }
  }
};
</script>

<style scoped>
</style>
