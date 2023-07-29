import { defineConfig } from 'vitepress'
import taskList from 'markdown-it-task-lists'
import { withMermaid } from 'vitepress-plugin-mermaid'

export default withMermaid(
    defineConfig({
      title: "Microscope boilerplate",
      description: "Documentation",
      srcDir: "./src",
      themeConfig: {
        search: {
          provider: 'local'
        },
        nav: [
          { text: 'Home', link: '/' },
          { text: 'Docs', link: '/Intro/introduction' }
        ],
    
        sidebar: [
          {
            text: 'Introduction',
            items: [
              { text: 'Introduction', link: '/Intro/introduction' },
              { text: 'Roadmap', link: '/Intro/roadmap' }
            ]
          },
            
          {
            text: 'Guidelines',
            items: [
              { text: 'Lean startup', link: '/Guidelines/lean-startup' },
              { text: 'Discovery & delivery', link: '/Guidelines/discovery-delivery' },
              { text: 'Product discovery', link: '/Guidelines/product-discovery' },
              { text: 'DDD Modelling process', link: '/Guidelines/ddd-modelling-process.md' },
              { text: 'Delivery process', link: '/Guidelines/delivery-process.md' }
            ]
          },
    
          {
            text: 'Architecture',
            items: [
              { text: 'Getting started', link: '/Architecture/getting-started' },
              { text: 'Microservices oriented', link: '/Architecture/microservices' },
              { text: 'Solution architecture', link: '/Architecture/architecture' },
              { text: 'Documentation', link: '/Architecture/getting-started' },
              { text: 'Tests', link: '/Architecture/getting-started' },
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
