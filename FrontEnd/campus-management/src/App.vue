<template>
  <v-app>
    <TheNavBar></TheNavBar>
    <v-content>
      <transition name="fade">
        <router-view></router-view>
      </transition>
      <!-- <v-footer height="auto" color="primary lighten-1" inset>
        <v-layout justify-center row wrap>
          <v-flex light-blue py-3 text-xs-center white--text xs12>
            &copy;2019 —
            <strong>CampusManagement</strong>
          </v-flex>
        </v-layout>
      </v-footer>-->
    </v-content>
    <v-snackbar
      v-model="snackbar.active"
      bottom
      right
      :timeout="snackbar.timeout"
      :color="snackbar.color"
    >
      {{ snackbar.text }}
      <v-btn color="white" flat @click="$store.dispatch('hideSnackbar')">Close</v-btn>
    </v-snackbar>
  </v-app>
</template>

<script>
import TheNavBar from "@/components/TheNavBar";

export default {
  name: "App",
  data() {
    return {
      drawer: true
    };
  },
  components: {
    TheNavBar
  },
  mounted() {
    this.$store.dispatch("getHostels");
  },
  computed: {
    snackbar() {
      return this.$store.getters.globalSnackbar;
    }
  }
};
</script>

<style>
.fade-enter-active,
.fade-leave-active {
  transition-property: opacity;
  transition-duration: 0.25s;
}

.fade-enter-active {
  transition-delay: 0.25s;
}

.fade-enter,
.fade-leave-active {
  opacity: 0;
}

textarea:focus,
input:focus {
  outline: none;
}

*:focus {
  outline: none;
}
</style>

