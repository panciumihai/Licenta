<template>
  <div></div>
</template>

<script>
export default {
  created() {
    this.$store
      .dispatch("destroyToken")
      // eslint-disable-next-line
      .then(response => {
        this.$router.push({ name: "home" });
      });
  }
};
</script>

<style>
</style>
