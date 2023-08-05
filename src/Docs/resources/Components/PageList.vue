<template>
  <div>
    <h2>Table of Contents</h2>
    <ul>
      <li v-for="page in folderPages">
        <p>{{ page }}</p>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  computed: {
    folderPages() {
      const allPages = [
          'test', 'another', 'okok'
      ];
      return allPages;
    },
  },
};
</script>
