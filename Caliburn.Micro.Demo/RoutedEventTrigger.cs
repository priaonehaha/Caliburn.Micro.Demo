//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Caliburn.Micro.Demo
// 
//  Caliburn.Micro.Demo is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Caliburn.Micro.Demo is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Caliburn.Micro.Demo. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Windows;
using System.Windows.Interactivity;

#endregion

namespace Caliburn.Micro.Demo
{
	public class RoutedEventTrigger : EventTriggerBase<DependencyObject>
	{
		public RoutedEvent RoutedEvent { get; set; }

		protected override string GetEventName()
		{
			return RoutedEvent.Name;
		}

		protected override void OnAttached()
		{
			var behavior = AssociatedObject as Behavior;
			var associatedElement = AssociatedObject as FrameworkElement;

			if (behavior != null)
			{
				associatedElement = ((IAttachedObject) behavior).AssociatedObject as FrameworkElement;
			}
			if (associatedElement == null)
			{
				throw new ArgumentException("Routed Event trigger can only be associated to framework elements");
			}
			if (RoutedEvent != null)
			{
				associatedElement.AddHandler(RoutedEvent, new RoutedEventHandler(OnRoutedEvent));
			}
		}

		private void OnRoutedEvent(object sender, RoutedEventArgs args)
		{
			OnEvent(args);
		}
	}
}