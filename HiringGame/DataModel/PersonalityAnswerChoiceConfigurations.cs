using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiringGame.DataModel
{
    public class PersonalityAnswerChoiceConfigurations:IEntityTypeConfiguration<PersonalityAnswerChoice>
    {
        public void Configure(EntityTypeBuilder<PersonalityAnswerChoice> builder)
        {
            
            var list=new List<PersonalityAnswerChoice>
            {
                new PersonalityAnswerChoice{Id = 1, Name = "MOST", ChoiceValue = 1},
                new PersonalityAnswerChoice{Id = 2, Name = "NOT SURE", ChoiceValue = 0},
                new PersonalityAnswerChoice{Id = 3, Name = "LEAST", ChoiceValue = -1},
            };

            builder.HasData(list);
        }
    }
}
