<template>
  <div class="news_preview">
    <v-container>
      <v-card @click="goToDetails">
        <v-layout row wrap>
          <v-flex xs12 md12 class="px-3 pt-3">
            <div class="title">{{ news.title }}</div>
          </v-flex>
          <v-flex xs12 md12 class="pb-3 px-3">
            <v-layout align-start justify-start>
              <div class="subheading">{{news.author}}</div>
              <div class="subheading font-weight-thin ml-1">a postat pe {{news.date}}</div>
            </v-layout>
          </v-flex>
          <v-flex xs12 md12>
            <v-img :src="news.image" :lazy-src="news.image" aspect-ratio="2.75">
              <v-layout row fill-height align-end>
                <div class="transparent">
                  <div class="pa-3 body-2 white--text">
                    {{ news.content.substring(0,200) }}...
                    <div
                      class="blue--text text--lighten-4 body-2 d-inline-flex"
                    >Click pe postare pentru a vedea mai mult!</div>
                  </div>
                </div>
              </v-layout>
            </v-img>
          </v-flex>
        </v-layout>
      </v-card>
    </v-container>
  </div>
</template>

<script>
export default {
  data() {
    return {
      news: {
        title: "Anunt important legat de cazarile de pe data de 22 septemrie",
        date: "22-02-2019",
        image: "http://campus.tuiasi.ro/wp-content/uploads/DSC_0173-min-1.jpg",
        author: "Popescu Marian",
        content:
          "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque sed ex tempus, iaculis lacus eu, consectetur ligula. Proin pharetra, ante vitae congue placerat, ipsum lacus maximus tellus, pretium imperdiet tellus urna in arcu. Vivamus elementum fringilla ex at ultricies. Nam volutpat tortor eget nisl viverra venenatis. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque sed ex tempus, iaculis lacus eu, consectetur ligula. Proin pharetra, ante vitae congue placerat, ipsum lacus maximus tellus, pretium imperdiet tellus urna in arcu. Vivamus elementum fringilla ex at ultricies. Nam volutpat tortor eget nisl viverra venenatis."
      }
    };
  },
  methods: {
    goToDetails: function() {
      this.$router.push({ name: "news" });
    }
  }
};
</script>

<style scoped>
.transparent {
  background-color: rgb(0, 0, 0, 0.65) !important;
  background: rgba(0, 0, 0, 0.65) !important;
}
</style>
