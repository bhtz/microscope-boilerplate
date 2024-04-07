import { defineConfig } from 'vitepress'
import taskList from 'markdown-it-task-lists'
import { withMermaid } from 'vitepress-plugin-mermaid'

export default withMermaid(
    defineConfig({
      title: "Microscope.Boilerplate",
      description: "Documentation",
      srcDir: "./resources",
      outDir: "../../docs",
      ignoreDeadLinks: true,
      themeConfig: {
        search: {
          provider: 'local'
        },
        nav: [
          { text: 'Home', link: '/' },
          { text: 'Documentation', link: '/getting-started' }
        ],
    
        sidebar: [
          {
            text: 'Overview',
            items: [
              { text: 'Getting started', link: '/getting-started' },
              { text: 'Roadmap', link: '/roadmap' }
            ]
          },

          {
            text: 'Tool',
            items: [
              { text: 'Installation', link: '/tool/installation' },
            ]
          },
            
          {
            text: 'Templates',
            items: [
              { text: 'Distributed', link: '/templates/distributed' },
              { text: 'BFF Blazor', link: '/templates/bff-ssr-blazor' },
              { text: 'Desktop', link: '/templates/desktop' },
              { text: 'CLI', link: '/templates/cli' },
              { text: 'Doc as code', link: '/templates/doc-as-code' },
            ]
          },
        ],
        socialLinks: [
          { icon: 'github', link: 'https://github.com/bhtz/microscope-boilerplate' }
        ]
      },
    
      markdown: {
        config: (md) => {
          md.use(taskList)
        }
      }
    })
)
