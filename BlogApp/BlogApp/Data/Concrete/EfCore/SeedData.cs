using Microsoft.EntityFrameworkCore;
using BlogApp.Entity;
using static BlogApp.Entity.Tag;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void FillTestData(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();

            if(context != null)
            {
                if(context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if(!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Tag { Text = "Container", Url="container",Color = TagColors.primary},
                        new Tag { Text = "Design Pattern", Url = "design-pattern",Color = TagColors.success},
                        new Tag { Text = "Frontend Development", Url ="frontend-development",Color = TagColors.danger},
                        new Tag { Text = "Backend Development", Url = "backend-development",Color = TagColors.secondary},
                        new Tag { Text = "Technology", Url = "technology",Color = TagColors.primary}
                    );

                    context.SaveChanges();
                }

                if(!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User {UserName ="gorkemkayas", Image ="ben.jpg", Title=".Net Specialist", Email ="gorkemkayas@hotmail.com",FirstName = "Görkem", SurName = "Kaya",Password = "gorkem123"},
                        new User {UserName ="17reyhan75",Image ="reyhankaya.jpg", Title="Financial advisor",Email ="katip@hotmail.com",FirstName = "Reyhan", SurName = "Kaya", Password = "reyhan123" },
                        new User {UserName ="gulsekyaaa",Image ="gulsenurkaya.jpg",Title="System Engineer",Email ="gulse147@hotmail.com", FirstName = "Gülse Nur",SurName="Kaya",Password = "gulse123"}
                    );

                    context.SaveChanges();
                }

                if(!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Post {Title = "Asp.net Core",
                        Content = "ASP.NET Core is an open-source modular web-application framework. It is a redesign of ASP.NET that unites the previously separate ASP.NET MVC and ASP.NET Web API into a single programming model. Despite being a new framework, built on a new web stack, it does have a high degree of concept compatibility with ASP.NET. The ASP.NET Core framework supports side-by-side versioning so that different applications being developed on a single machine can target different versions of ASP.NET Core. This was not possible with previous versions of ASP.NET. ASP.NET Core initially ran on both the Windows-only .NET Framework and the cross-platform .NET. However, support for the .NET Framework was dropped beginning with ASP.Net Core 3.0. Blazor is a recent (optional) component to support WebAssembly and since version 5.0, it has dropped support for some old web browsers. While current Microsoft Edge works, the legacy version of it, i.e. 'Microsoft Edge Legacy' and Internet Explorer 11 was dropped when you use Blazor.",
                        isActive = true,
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Take(3).ToList(),
                        UserId  = 1,
                        Image = "aspcore.png",
                        Url = "asp-net-core",
                        Comments = new List<Comment>
                        {
                            new Comment {CommentText = "I would say thank you about your helpful topic, I was confused before read your topic but now everything is perfect!", PublishedOn = DateTime.Now.AddDays(-1), UserId = 2},
                            new Comment {CommentText = "Researched, compiled and organized content, It's a hard to find caring people like you.", PublishedOn = DateTime.Now.AddDays(-2), UserId = 3},
                        }
                        },
                        new Post {Title = "Java",
                        Content = "Java is a high-level, class-based, object-oriented programming language that is designed to have as few implementation dependencies as possible. It is a general-purpose programming language intended to let programmers write once, run anywhere (WORA), meaning that compiled Java code can run on all platforms that support Java without the need to recompile. \nJava applications are typically compiled to bytecode that can run on any Java virtual machine (JVM) regardless of the underlying computer architecture. Although its syntax is similar to that of C and C++, the Java language has fewer low-level facilities than either of them. The Java runtime provides dynamic capabilities (such as reflection and runtime code modification) that are typically not available in traditional compiled languages.Java gained popularity shortly after its release, and has been a very popular programming language since then. Java was the third most popular programming language in 2022 according to GitHub. Although still widely popular, there has been a gradual decline in use of Java in recent years with other languages using JVM gaining popularity. \nJava was originally developed by James Gosling at Sun Microsystems. It was released in May 1995 as a core component of Sun's Java platform. The original and reference implementation Java compilers, virtual machines, and class libraries were originally released by Sun under proprietary licenses. As of May 2007, in compliance with the specifications of the Java Community Process, Sun had relicensed most of its Java technologies under the GPL-2.0-only license. Oracle offers its own HotSpot Java Virtual Machine, however the official reference implementation is the OpenJDK JVM which is free open-source software and used by most developers and is the default JVM for almost all Linux distributions.As of March 2024, Java 22 is the latest version. Java 8, 11, 17, and 21 are previous LTS versions still officially supported.",
                        isActive = true,
                        PublishedOn = DateTime.Now.AddDays(-2),
                        Tags = context.Tags.Take(2).ToList(),
                        UserId  = 1,
                        Image = "java.jpeg",
                        Url = "java"
                        },
                        new Post {Title = "Python",
                        Content = "Python is a high-level, general-purpose programming language. Its design philosophy emphasizes code readability with the use of significant indentation. Python is dynamically typed and garbage-collected. It supports multiple programming paradigms, including structured (particularly procedural), object-oriented and functional programming. It is often described as a 'batteries included' language due to its comprehensive standard library. \nGuido van Rossum began working on Python in the late 1980s as a successor to the ABC programming language and first released it in 1991 as Python 0.9.0. Python 2.0 was released in 2000. Python 3.0, released in 2008, was a major revision not completely backward-compatible with earlier versions. Python 2.7.18, released in 2020, was the last release of Python 2. Python consistently ranks as one of the most popular programming languages, and has gained widespread use in the machine learning community.",
                        isActive = true,
                        PublishedOn = DateTime.Now.AddDays(-24),
                        Tags = context.Tags.Take(1).ToList(),
                        UserId  = 3,
                        Image = "python.webp",
                        Url = "Python"
                        },
                        new Post {Title = "Kubernetes",
                        Content = "Kubernetes is an open-source container orchestration system for automating software deployment, scaling, and management. Originally designed by Google, the project is now maintained by a worldwide community of contributors, and the trademark is held by the Cloud Native Computing Foundation.The name Kubernetes originates from Ancient Greek, meaning 'helmsman' or 'pilot'. Kubernetes is often abbreviated as K8s, counting the eight letters between the K and the s (a numeronym).Kubernetes assembles one or more computers, either virtual machines or bare metal, into a cluster which can run workloads in containers. It works with various container runtimes, such as containerd and CRI-O. Its suitability for running and managing workloads of all sizes and styles has led to its widespread adoption in clouds and data centers. There are multiple distributions of this platform  from independent software vendors (ISVs) as well as hosted-on-cloud offerings from all the major public cloud vendors.\nKubernetes is one of the most widely deployed software systems in the world[9] being used across companies including Google, Microsoft, Amazon, Apple, Meta, Nvidia, Reddit and Pinterest.",
                        isActive = true,
                        PublishedOn = DateTime.Now.AddDays(-4),
                        Tags = context.Tags.Take(3).ToList(),
                        UserId  = 2,
                        Image = "kubernetes.jpg",
                        Url = "kubernetes"
                        },
                        new Post {Title = "Docker",
                        Content = "Docker is a set of platform as a service (PaaS) products that use OS-level virtualization to deliver software in packages called containers. The service has both free and premium tiers. The software that hosts the containers is called Docker Engine.     It was first released in 2013 and is developed by Docker, Inc. Docker is a tool that is used to automate the deployment of applications in lightweight containers so that applications can work efficiently in different environments in isolation.",
                        isActive = true,
                        PublishedOn = DateTime.Now.AddDays(-17),
                        Tags = context.Tags.Take(2).ToList(),
                        UserId  = 1,
                        Image = "docker.jpg",
                        Url="docker"
                        },
                        new Post {Title = "React",
                        Content = "React (also known as React.js or ReactJS) is a free and open-source front-end JavaScript library for building user interfaces based on components. It is maintained by Meta (formerly Facebook) and a community of individual developers and companies.React can be used to develop single-page, mobile, or server-rendered applications with frameworks like Next.js. Because React is only concerned with the user interface and rendering components to the DOM, React applications often rely on libraries for routing and other client-side functionality. A key advantage of React is that it only rerenders those parts of the page that have changed, avoiding unnecessary rerendering of unchanged DOM elements.",
                        isActive = true,
                        PublishedOn = DateTime.Now.AddDays(-1),
                        Tags = context.Tags.Take(3).ToList(),
                        UserId  = 3,
                        Image = "react.png",
                        Url="react"
                        }
                        ,
                        new Post {Title = "Go",
                        Content = "Go is a statically typed, compiled high-level programming language designed at Google by Robert Griesemer, Rob Pike, and Ken Thompson. It is syntactically similar to C, but also has memory safety, garbage collection, structural typing, and CSP-style concurrency. It is often referred to as Golang because of its former domain name, golang.org, but its proper name is Go.\nThere are two major implementations:\nGoogle's self-hosting 'gc' compiler toolchain, targeting multiple operating systems and WebAssembly.go frontend, a frontend to other compilers, with the libgo library. With GCC the combination is gccgo; with LLVM the combination is gollvm. A third-party source-to-source compiler, GopherJS, compiles Go to JavaScript for front-end web development.",
                        isActive = true,
                        PublishedOn = DateTime.Now.AddDays(-2),
                        Tags = context.Tags.Take(2).ToList(),
                        UserId  = 2,
                        Image = "go.jpeg",
                        Url="go"
                        }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}