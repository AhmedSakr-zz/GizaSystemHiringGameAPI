using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiringGame.DataModel
{
    public class QuestionTypeConfigurations:IEntityTypeConfiguration<QuestionType>
    {
        public void Configure(EntityTypeBuilder<QuestionType> builder)
        {
            
            var list=new List<QuestionType>
            {
                new QuestionType{Id = 1,Name = "Level 2"},
                new QuestionType{Id = 2,Name = "Level 3"}
            };
            builder.HasData(list);
        }
    }
}
