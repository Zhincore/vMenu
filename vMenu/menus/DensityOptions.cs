using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuAPI;
using Newtonsoft.Json;
using CitizenFX.Core;
using static CitizenFX.Core.UI.Screen;
using static CitizenFX.Core.Native.API;
using static vMenuClient.CommonFunctions;
using static vMenuShared.PermissionsManager;
using vMenuShared;

namespace vMenuClient
{
    public class DensityOptions
    {
        // Variables
        private Menu menu;

        /// <summary>
        /// Creates the menu.
        /// </summary>
        private void CreateMenu()
        {
            // Create the menu.
            menu = new Menu(Game.Player.Name, "Density Options");

            MenuSliderItem vehicleDensity = new MenuSliderItem("Vehicle density", 0, 10, ((int)(ConfigManager.GetSettingsFloat(ConfigManager.Setting.vmenu_current_vehicle_density) * 10f)));
            MenuSliderItem pedDensity = new MenuSliderItem("Pedestrian density", 0, 10, ((int)(ConfigManager.GetSettingsFloat(ConfigManager.Setting.vmenu_current_ped_density) * 10f)));


            // Add all menu items to the menu.

            menu.AddMenuItem(vehicleDensity);
            menu.AddMenuItem(pedDensity);


            // Handle button presses.
            menu.OnSliderPositionChange += (m, sliderItem, oldPosition, newPosition, itemIndex) =>
            {
                if (sliderItem == vehicleDensity)
                {
                    TriggerServerEvent("vMenu:UpdateServerVehicleDensity", ((float) newPosition) / 10);
                }
                else if (sliderItem == pedDensity)
                {
                    TriggerServerEvent("vMenu:UpdateServerPedDensity", ((float) newPosition) / 10);
                }
            };
        }

        /// <summary>
        /// Create the menu if it doesn't exist, and then returns it.
        /// </summary>
        /// <returns>The Menu</returns>
        public Menu GetMenu()
        {
            if (menu == null)
            {
                CreateMenu();
            }
            return menu;
        }
    }
}
