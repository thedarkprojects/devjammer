---
title: Home
category: Main Menu
order: 1
tags: [Cronux, DevJammer, Thecarisma, Windows]
years: 2019–present
tile-header: front_image.png
tile: front_image.png
links:
  
---
home

—
<ul>
{% for link in page.links %}
  <li>{{ link | markdownify | remove: "<p>" | remove: "</p>" }}</li>
{% endfor %}
</ul>
