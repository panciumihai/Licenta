<template>
  <v-app>
    <TheNavBar></TheNavBar>
    <v-content>
      <transition name="fade">
        <router-view></router-view>
      </transition>
    </v-content>
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

