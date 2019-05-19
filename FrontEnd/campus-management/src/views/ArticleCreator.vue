<template>
  <div id="pageEditor">
    <v-container>
      <v-card>
        <v-card-title class="title mb-1">Scrieti anuntul dorit</v-card-title>
        <v-card-text class="mt-0">
          <v-layout column wrap>
            <v-flex>
              <v-text-field v-model="article.title" prepend-icon="title" label="Titlu"></v-text-field>
            </v-flex>
            <v-flex align-center align-content-center justify-center align-content-space-around>
              <img :src="article.imageUrl" aspect-ratio="2.75" v-if="article.imageUrl">
            </v-flex>
            <v-flex>
              <v-text-field
                label="Alege o imagine"
                @click="pickFile"
                v-model="article.imageName"
                prepend-icon="attach_file"
              ></v-text-field>
              <input
                type="file"
                style="display: none"
                ref="image"
                accept="image/*"
                @change="onFilePicked"
              >
            </v-flex>
            <v-flex>
              <quill-editor v-model="article.content" :options="config" output="html"></quill-editor>
            </v-flex>
            <v-flex>
              <v-layout justify-center>
                <v-btn class="mt-3" dark color="blue" @click="addArticle()">Posteaza</v-btn>
              </v-layout>
            </v-flex>
          </v-layout>
        </v-card-text>
      </v-card>
    </v-container>
  </div>
</template>

<script>
export default {
  components: {},
  data() {
    return {
      config: {
        placeholder: "Scrieti articolul dorit"
      },
      article: {
        title: "",
        content: "",
        imageName: "",
        imageUrl: "",
        imageFile: ""
      }
    };
  },
  methods: {
    pickFile() {
      this.$refs.image.click();
    },

    onFilePicked(e) {
      const files = e.target.files;
      if (files[0] !== undefined) {
        this.article.imageName = files[0].name;
        if (this.article.imageName.lastIndexOf(".") <= 0) {
          return;
        }
        const fr = new FileReader();
        fr.readAsDataURL(files[0]);
        fr.addEventListener("load", () => {
          this.article.imageUrl = fr.result;
          this.article.imageFile = files[0]; // this is an image file that can be sent to server...
        });
      } else {
        this.article.imageName = "";
        this.article.imageFile = "";
        this.article.imageUrl = "";
      }
    },
    addArticle() {
      let rawData = {
        title: this.article.title,
        content: this.article.content
      };
      rawData = JSON.stringify(rawData);
      let formData = new FormData();
      formData.append("file", this.article.imageFile, this.article.imageName);
      formData.append("articleLightCreateModel", rawData);

      this.$store.dispatch("addArticle", formData);
    }
  },
  created: {}
};
</script>

<style>
</style>
