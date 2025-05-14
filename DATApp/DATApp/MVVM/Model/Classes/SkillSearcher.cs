using System;
using System.Collections.Generic;
using System.Linq;


	namespace DATApp.MVVM.Model.Classes

{
	public class SkillSearcher
	{
		private List<Skill> Skills { get; set; }
	}

	
	public void SetSkills(IEnumerable<Skill> skills)
    {
			Skills = skills.ToList();

	}
		public IEnumerbale<SearchResult> Search (string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
				return Enumerable.Empty<SearchResult>();

			var results = Skills
			.Where(p =>
					(!string.IsNullOrEmpty(p.Description) && p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
					(!string.IsNullOrEmpty(p.Name) && p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
				.Select(p => new SearchResult
				{
					Category = "Skill",
					Description = p.Name,
					ID = p.SKillNumber,
					OriginatingSKill = p,
					SkillInformation = GetSKillInfo(p.SKillNumber)

				})

				.ToList();
		}

		private static Dictionary<string, string> GetSkillInfo(int id)
		{
			return new Dictionary<string, string>
			{
				{ "controller", "skill" },
				{ "action", "details" },
				{ "ID", id.ToString() }

			};

		}

     }

}