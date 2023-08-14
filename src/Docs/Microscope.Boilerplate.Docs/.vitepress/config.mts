import { defineConfig } from 'vitepress'
import taskList from 'markdown-it-task-lists'
import { withMermaid } from 'vitepress-plugin-mermaid'

export default withMermaid(
    defineConfig({
      title: "Microscope boilerplate",
      description: "Documentation",
      srcDir: "./resources",
      outDir: "./wwwroot",
      ignoreDeadLinks: true,
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
            text: 'Product & Tech Discovery',
            items: [
              { text: 'Discovery framing', link: '/Product/toc' },
              { text: 'Event storming', link: '/Architecture/EventStorming/toc' },
              { text: 'Bounded Context Canvas', link: '/Architecture/BoundedContexts/toc' },
            ]
          },
    
          {
            text: 'Architecture',
            items: [
              { text: 'Getting started', link: '/Architecture/getting-started' },
              { text: 'Solution structure', link: '/Architecture/solution-structure' },
              { text: 'Architecture schema', link: '/Architecture/architecture' },
              { text: 'Technology matrix', link: '/Architecture/technologies' },
            ]
          },

          {
            text: 'Organization & Governance',
            items: [
              { text: 'Squads', link: '/Organization/Squads/toc' },
              { text: 'Weekly meetings', link: '/Organization/Governance/Weekly/toc' },
              { text: 'Architecture Decision Record', link: '/Organization/Governance/ADR/toc' },
            ]
          },

          {
            text: 'Guidelines',
            items: [
              { text: 'Lean startup', link: '/Guidelines/lean-startup' },
              { text: 'Discovery & delivery', link: '/Guidelines/discovery-delivery' },
              { text: 'Product discovery', link: '/Guidelines/product-discovery' },
              { text: 'DDD Modelling process', link: '/Guidelines/ddd-modelling-process.md' },
              { text: 'Microservices oriented', link: '/Guidelines/microservices' },
              { text: 'Delivery process', link: '/Guidelines/delivery-process.md' },
              { text: 'No estimate', link: '/Guidelines/no-estimate.md' },
              { text: 'Well Architected Framework', link: '/Guidelines/waf.md' },
              { text: 'Matrix management', link: '/Guidelines/matrix-management.md' },
              { text: 'Career path', link: '/Guidelines/career-path.md' },
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
