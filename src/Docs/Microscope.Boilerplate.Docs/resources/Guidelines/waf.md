# Well Architecture Framework

## 5 pillars of Well Architected Framework

- Not something to balance, or trade-offs, they're a synergy
    - Help you track your performance and evolve your architecture.
- 5 pillars summary:
    1. **Operational Excellence**
        - **Description**: Run and monitor systems & continually improve supporting processes and procedures
        - **Design principles**
            - **Perform operations as code**: Infrastructure as Code
            - **Annotate documentation**: e.g. auto-document after every build)*
            - **Make frequent, small, reversible changes**
            - **Refine operations procedures frequently**: Ensure team members are familiar with it.
            - **Anticipate failure**: Learn from all operational failures
        - **Example AWS Services**
            - **Selection**: Auto Scaling, Lambda, EBS, S3, RDS
            - **Review**: AWS CloudFormation, [AWS News Blog](https://aws.amazon.com/blogs/aws/)
            - **Monitoring**: CloudWatch, Lambda
            - **Tradeoffs**: Amazon RDS (vs Aurora), ElastiCache (read performance but stale), Snowball (a lot of data but takes a week), CloudFront (cache but stale)
    2. **Security**
        - **Description**: Protect information, systems, and assets through risk assessments and mitigation strategies
        - **Design principles**
            - **Implement a strong identity foundation**: Least privilege, IAM, centralize privilege management, eliminate reliance on long-term credentials
            - **Enable traceability**: Automatically respond to logs and metrics.
            - **Apply security at all layers**: Every instance, OS, and app, e.g. VPC, subnet, LB.
            - **Automate security best practices**: Encryption, tokenization, and access control.
            - **Protect data in transit and at rest**
            - **Keep people away from data**: Reduce the need for direct access or manual data processing
            - **Prepare for security**: Run incident response simulations and use tools with automation to increase your speed for detection, investigation, and recovery
        - **Example AWS Services**
        - **Identity and access management**: IAM, AWS-STS, MFA token, AWS Organizations
        - **Detective controls**: AWS Config, AWS CloudTrail, Amazon CLoudWatch
        - **Infrastructure Protection**
            - **Amazon CloudFront**: Defense against DDoS attack
            - Amazon VPC
            - **AWS Shield**: DDoS protection of AWS account
            - **AWS WAF**: Web Application Firewall
            - **Amazon Inspector**: for security of EC2 instance
        - **Data protection**: KMS, S3 (SSE, SSE-KMS, SSE-C, bucket policies etc.)
            - And other managed services such as Elastic Load Balancing (e.g. only HTTPS), Amazon EBS & RDS (SSL Capability)
        - **Incident Response**
            - **IAM**: Delete account or give it zero privilege
            - **CloudFormation**: If someone deletes entire structure, how to get back?
            - Amazon CloudWatch Events
    3. **Reliability**
        - **Description**:
            - Ability of a system to recover from infrastructure or service disruptions
            - Dynamically acquire computing resources to meet demand
            - Mitigate disruptions such as misconfigurations or transient network issues.
        - **Design principles**
            - **Test recovery producers**: Use automation to simulate different failures or to recreate scenarios that led to failures before
            - **Automatically recover from failure**: Anticipate and remediate failures before they occur.
            - **Scale horizontally to increase aggregate system availability**: Distribute requests across multiple, smaller resources to ensure that they don't share a common point of failure
            - **Stop guessing capacity**: Maintain the optimal level to satisfy demand without over or under provisioning, use auto-scaling.
            - **Manage change in automation**: Use automation to make changes to infrastructure
        - **Example AWS Services**
            - **Foundations**
                - **IAM**: No one has too many rights to damage
                - Amazon VPC
                - **Service limits**: monitor limits so you don't get disruption.
                - **AWS Trusted Advisor**: e.g. look at service limits
            - **Change Management**: AWS Auto Scaling, Amazon CloudWatch, AWS CloudTrail, AWS Config
            - **Failure Management**: Backups, AWS CloudFormation (recreate whole infrastructure at once), Amazon S3, Amazon S3 Glacier, Amazon Route 53 (e.g. if application fails you redirect to another application)
    4. **Performance**
        - **Description**: Adopt & provide best performance
        - **Design principles**
            - Democratize advanced technologies *(track new services)*
            - Go global in minutes *(easy multi-region deployment)*
            - Use serverless architectures *(avoid burden of managing servers)*
            - Experiment more often *(easy to carry out comparative testing)*
            - Mechanical sympathy *(be aware of all AWS services)*
        - **Example AWS Services**
            - **Prepare**: AWS CloudFormation, AWS Config
            - **Operate**: AWS CloudFormation, AWS Config, AWS CloudTrail, Amazon CloudWatch, AWS X-Ray
            - **Evolve**: AWS CloudFormation, CI/CD: CodeBuild, CodeCommit, CodeDeploy, CodePipeline
    5. **Cost Optimization**
        - **Description**: Can run systems to deliver business value at the lowest price point.
        - **Design Principles**
            - **Adopt a consumption mode**: Pay only for what you use
            - **Measure overall efficiency**: Use CloudWatch
            - **Analyze and attribute expenditure**: Use tags, Accurate identification of system usage and costs -> helps measure return on investment (ROI)
            - **Use managed and application level services to reduce cost of ownership**: As managed services operate at cloud scale, they can offer a lower cost per transaction or service
        - **Example AWS Services**
            - **Expenditure awareness**: AWS Budgets, AWS Cost and Usage Report, AWS Cost Explorer, Reserved Instance Reporting
            - **Cost-effective resources**: Spot instance, Reserved instance, Amazon S3 Glacier
            - **Matching supply and demand**: AWS Auto Scaling, AWS Lambda
            - **Optimizing over time**: AWS Trusted Advisor, AWS Cost and Usage Report, [AWS News Blog](https://aws.amazon.com/blogs/aws/)

## Well Architected Tool

- [Tool](https://console.aws.amazon.com/wellarchitected)
- Flow:
    1. Define a workload
    2. Do a review
        - Answer questions for each pillar.
        - E.g. for operational excellence
            - Question: How do you determine what your priorities are?
            - Answers: customer needs / compliance requirements / ... / none
    3. Optionally generate reports, milestones, improvement plans (with risks, e.g. "understand business needs").