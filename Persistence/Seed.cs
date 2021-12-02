using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            var leafOneId = Guid.NewGuid();
            var leafTwoId = Guid.NewGuid();
            var leafThreeId = Guid.NewGuid();
            var leafFourId = Guid.NewGuid();

            var nodeOneId = Guid.NewGuid();
            var nodeTwoId = Guid.NewGuid();
            var nodeThreeId = Guid.NewGuid();
            var nodeFourId = Guid.NewGuid();
            
            var firstNode = new TreeNode{
                Id = nodeOneId,
                    Name = "First Node",
                    ParentId = null,
                    Children = new List<TreeNode>()
            };

            var secondNode = new TreeNode{
                Id = nodeTwoId,
                    Name = "Second Node",
                    ParentId = nodeOneId,
                    Children = new List<TreeNode>()
            };


            var thirdNode = new TreeNode{
                Id = nodeThreeId,
                    Name = "Third Node",
                    ParentId = nodeOneId,
                    Children = new List<TreeNode>()
            };

            var fourthNode = new TreeNode{
                Id = nodeFourId,
                    Name = "Fourth Node",
                    ParentId = nodeTwoId,
                    Children = new List<TreeNode>()
            };

            firstNode.Children.Add(secondNode);
            firstNode.Children.Add(thirdNode);
            secondNode.Children.Add(fourthNode);

            var nodes = new List<TreeNode>();
            nodes.Add(firstNode);
            nodes.Add(secondNode);
            nodes.Add(thirdNode);
            nodes.Add(fourthNode);
            
            var leafs = new List<Leaf>
            {
                new Leaf
                {
                    Id = leafOneId,
                    Name = "First Leaf",
                    Title = "First Leaf",
                    Text = "First Leaf",
                    ParentId = nodeOneId
                },
                new Leaf
                {
                    Id = leafTwoId,
                    Name = "Second Leaf",
                    Title = "Second Leaf",
                    Text = "Second Leaf",
                    ParentId = nodeOneId
                },
                new Leaf
                {
                    Id = leafThreeId,
                    Name = "Third Leaf",
                    Title = "Third Leaf",
                    Text = "Third Leaf",
                    ParentId = nodeTwoId
                },
                new Leaf
                {
                    Id = leafFourId,
                    Name = "Fourth Leaf",
                    Title = "Fourth Leaf",
                    Text = "Fourth Leaf",
                    ParentId = nodeThreeId
                }
            };
            await context.Nodes.AddRangeAsync(nodes);
            await context.Leafs.AddRangeAsync(leafs);
            await context.SaveChangesAsync();
        }
    }
}
