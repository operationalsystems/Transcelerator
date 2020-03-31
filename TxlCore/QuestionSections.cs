// ---------------------------------------------------------------------------------------------
#region // Copyright (c) 2020, SIL International.
// <copyright from='2011' to='2020' company='SIL International'>
//		Copyright (c) 2020, SIL International.
//
//		Distributable under the terms of the MIT License (http://sil.mit-license.org/)
// </copyright>
#endregion
//
// File: QuestionSections.cs
// ---------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SIL.Transcelerator
{
	#region class QuestionSections
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true)]
	[XmlRoot("ComprehensionCheckingQuestions", Namespace = "", IsNullable = false)]
	public class QuestionSections : ICloneable
	{
		[XmlElement("Section", typeof(Section), Form = XmlSchemaForm.Unqualified)]
		public Section[] Items { get; set; }

		public QuestionSections Clone() => (QuestionSections)((ICloneable)this).Clone();

		object ICloneable.Clone()
		{
			var clone = new QuestionSections {Items = new Section[Items.Length]};
			for (var index = 0; index < Items.Length; index++)
				clone.Items[index] = Items[index].Clone();

			return clone;
		}
	}

	#endregion

	#region class Section
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true)]
	public class Section : IRefRange, ICloneable
	{
		[XmlAttribute("heading")]
		public string Heading { get; set; }

		[XmlAttribute("scrref")]
		public string ScriptureReference { get; set; }

		[XmlAttribute("startref")]
		public int StartRef { get; set; }

		[XmlAttribute("endref")]
		public int EndRef { get; set; }

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Category", typeof(Category), IsNullable = false)]
		public Category[] Categories { get; set; }

		public Section Clone() => (Section)((ICloneable)this).Clone();

		object ICloneable.Clone()
		{
			var clone = (Section)MemberwiseClone();
			clone.Categories= new Category[Categories.Length];
			for (var index = 0; index < Categories.Length; index++)
				clone.Categories[index] = Categories[index].Clone();
			return clone;
		}
	}
	#endregion

	#region class Category
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true)]
	public class Category : ICloneable
	{
		[XmlAttribute("overview")]
		public bool IsOverview { get; set; }

		[XmlAttribute("type")]
		public string Type { get; set; }

	    private List<Question> m_questions;
		[XmlElement]
        public List<Question> Questions => m_questions ?? (m_questions = new List<Question>());

        public Category Clone() => (Category)((ICloneable)this).Clone();

        object ICloneable.Clone()
        {
	        var clone = (Category)MemberwiseClone();
	        if (m_questions != null)
	        {
				clone.m_questions = new List<Question>(m_questions.Count);
		        foreach (var q in m_questions)
			        clone.m_questions.Add(q.Clone());
	        }

	        return clone;
        }
	}
	#endregion
}
