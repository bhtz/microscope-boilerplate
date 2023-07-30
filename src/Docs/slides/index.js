import Reveal from 'reveal.js/dist/reveal.esm.js'
import RevealHighlight from 'reveal.js/plugin/highlight/highlight.esm.js'
import RevealMarkdown from 'reveal.js/plugin/markdown/markdown.esm.js'

import 'reveal.js/dist/reveal.css'
// import 'reveal.js/dist/theme/white.css'
// import 'reveal.js/dist/theme/black.css'
// import 'reveal.js/dist/theme/league.css'
import 'reveal.js/dist/theme/moon.css'
import 'reveal.js/plugin/highlight/monokai.css'

Reveal.initialize({
    hash: true,
    plugins: [RevealMarkdown, RevealHighlight]
});