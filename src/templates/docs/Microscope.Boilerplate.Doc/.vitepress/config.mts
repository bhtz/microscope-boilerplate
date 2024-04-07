import { defineConfig } from 'vitepress'
import taskList from 'markdown-it-task-lists'
import { withMermaid } from 'vitepress-plugin-mermaid'

export default withMermaid(
    defineConfig({
      title: "Microscope.Boilerplate",
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
          { text: 'Docs', link: '/Overview/introduction' }
        ],
    
        sidebar: [
          {
            text: 'Overview',
            items: [
              { text: 'Introduction', link: '/Overview/introduction' },
            ]
          },

//#if( Product )
          {
            text: 'Product documentation',
            items: [
              { text: 'Objective Key Results (OKR)', link: '/Product/OKR' },
              { text: 'Product Requirement Document', link: '/Product/PRD/toc' },
              { text: 'Roadmap', link: '/Product/roadmap' },
            ]
          },
//#endif
            
//#if( Tech )
          {
            text: 'Tech documentation',
            items: [
              { text: 'Installation', link: '/Architecture/installation' },
              { text: 'Solution structure', link: '/Architecture/solution-structure' },
              { text: 'Architecture schema', link: '/Architecture/architecture' },
              { text: 'Technology matrix', link: '/Architecture/technologies' },
              { text: 'Bounded Context Canvas', link: '/Architecture/BoundedContexts/toc' },
              { text: 'Architecture Decision Record', link: '/Architecture/ADR/toc' },
              { text: 'Event storming', link: '/Architecture/EventStorming/toc' },
            ]
          },
//#endif

//#if( Organization )
          {
            text: 'Organization & Governance',
            items: [
              { text: 'Squads', link: '/Organization/Squads/toc' },
              { text: 'Weekly meetings', link: '/Organization/Governance/Weekly/toc' },
            ]
          },
//#endif
            
//#if( Blog )
          {
            text: 'Blog',
            items: [
              { text: 'Welcome', link: '/Blog/welcome' },
            ]
          },
//#endif

//#if( Guidelines )
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
          }
//#endif
            
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
