<template>
  <div class="home">
    <h1>Home page</h1>
    <v-btn class="indigo white--text">CLICKEAZA-MA</v-btn>
    <Popup></Popup>
  </div>
</template>

<script>
import Popup from "@/components/Popup";

export default {
  components: {
    Popup
  }
};
</script>
