WPC CSharp driver release changelog
===================================

v0.2.6 Date: 2023/09/26, Developer: Chunglee_people
---------------------------------------------------
### Added
- New modules: Counter and PWM
- New example code: Counter and PWM
- table_channel.md

### Changed
- USB DAQ series's pinout with PWM & channel supported

v0.2.5 Date: 2023/09/06, Developer: Chunglee_people
---------------------------------------------------
### Added
- API: Wifi_setBandwidth

### Fixed
- README.md

v0.2.4 Date: 2023/07/18, Developer: Chunglee_people
---------------------------------------------------
### Added
- New product: `EthanI` & `EthanT`

v0.2.3 Date: 2023/07/17, Developer: Chunglee_people
---------------------------------------------------
### Added
- New product: `EDrive_ST`

v0.2.2 Date: 2023/06/08, Developer: Chunglee_people
---------------------------------------------------
### Added
- New product: `WifiDAQF4A`
- Add STEM port supplementary note

### Fixed
- `STEM` has not supported `AO_waveform_gen` yet
- If the product is `STEM`, use slot instead of port

v0.2.1 Date: 2023/06/05, Developer: Chunglee_people
---------------------------------------------------
### Added
- STEM product introduction
- Add `STEM` in handle list

### Changed
- Wifi DAQ AI port to 0
- STEM product images
- API name:
  - Wifi_readAccleration -> Wifi_readAcceleration

### Fixed
- Firmware error code

v0.2.0 Date: 2023/06/03, Developer: Chunglee_people
---------------------------------------------------
### Added
- Add STEM product

v0.1.8 Date: 2023/03/25, Developer: Chunglee_people
---------------------------------------------------
### Added
- Release new API :
  - Motion_enableServoOff, Motion_enableServoOn, Motion_getLogicalPosi, Motion_getEncoderPosi
  - Relay_open, Relay_close
- Example codes:
  - Relay_read_counters, Relay_set_channel, Motion_servo_on, Motion_position_blending
- Add `[0x00, 0x00, 0x55, 0x05]` packet list in _Motion_setPIOSignal1
- Add default value in example code
- Add Datalogger in each product

### Changed
- Change DataloggerModule API return type to int
- WifiDAQE3A product image

### Fixed
- Motion Axis value
  - MOT_AXIS0: 0
  - MOT_AXIS1: 1
  - MOT_AXIS2: 2
  - MOT_AXIS3: 3

### Removed
- Remove timeout parameter in CAN_start
- DIO series example code in EthanL
- DateLogger in product

v0.1.7 Date: 2023/03/09, Developer: Chunglee_people
---------------------------------------------------
### Added
- Add `serial_num ='default'` in USB series product
- Add `ip= '192.168.5.79'` in Wifi series product
- Add `ip= '192.168.1.110'` in Ethan series product

### Changed
- Sync C# & python in example code

### Fixed
- Fix zip download website

v0.1.6 Date: 2023/02/28, Developer: Chunglee_people
---------------------------------------------------
### Fixed
- Fix I2C error

v0.1.5 Date: 2023/01/18, Developer: Chunglee_people
---------------------------------------------------
### Fixed
- Fix example code
- Fix Motion API and its description
- Update error code

v0.1.4 Date: 2023/01/06, Developer: Chunglee_people
----------------------------------------------------
### Added
- Example codes:
   - Motion_find_home.cs, Motion_find_limit.cs and Motion_find_index.cs,
   - Motion_velocity_blending.cs and Motion_velocity_blending_accerleration,
   - Motion_load_configuration_file.cs and Motion_set_configuration_file,
   - Motion_1axis_move_with_alarm_in.cs, Motion_1axis_move_with_inposition.cs, Motion_1axis_move_with_breakpoint.cs,
	 Motion_1axis_move_with_configuration_file.cs, Motion_1axis_move_with_capture.cs and Motion_1axis_move_with_S_curve_acceleration.cs
   - Motion_2axis_circular_interpolation.cs and Motion_2axis_linear_interpolation.cs
   - Motion_3axis_linear_interpolation.cs and Motion_3axis_helical_interpolation.cs
   - Motion_3axis_synchronous_move.cs and Motion_3axis_asynchronous_move.cs
- Add EMotion into WPC_example_code.cs
### Removed
- WPC_7steps_Of_Python_Project_Workflow_r6.pdf


v0.1.3 Date: 2022/12/13, Developer: Chunglee_people
----------------------------------------------------
### Added
- WPC_MCX_H_Motion_Manual_r24.pdf
- Add motion one axis move example code `Motion_get_logical_position.cs`
- Add Emotion product into I/O Function Table

v0.1.2 Date: 2022/12/06, Developer: Chunglee_people
----------------------------------------------------
### Added
- Add motion simple example code `Motion_one_axis_move.cs`
- Add I/O function table in docfx

### Changed
- Uniform USB DAQ name `F1`
- Update `WPC_DAQ_Devices_User_Manual` from r20 to r22

v0.1.1 Date: 2022/12/01, Developer: Chunglee_people
----------------------------------------------------
### Added
- Release new API `DO_togglePins` and `DO_togglePort`
- Decorate docfx mainpage

v0.1.0 Date: 2022/11/24, Developer: Chunglee_people
----------------------------------------------------
### Added
- portal example code
- Add Datalogger example code
- Add product `EthanL` & `EthanO`
- Add `AO_waveform_generation.cs` example code

### Changed
- Update `WPC_DAQ_Devices_User_Manual` from r19 to r20

v0.0.13 Date: 2022/11/17, Developer: Chunglee_people
----------------------------------------------------
### Added
- *.* linguist-language=C# .gitattributes
- Add nuget
- Decorate docfx

v0.0.12 Date: 2022/11/15, Developer: Chunglee_people
----------------------------------------------------
### Added
- doxcfx_project

### Removed
- Doxygen relative files
- Doxyfile

v0.0.11 Date: 2022/11/11, Developer: Chunglee_people
------------------------------------------------------
### Added
- Add example code with its product

v0.0.10.2 Date: 2022/11/09, Developer: Chunglee_people
------------------------------------------------------
### Added
- Add `custom` folder which contains DoxygenLayout.xml, headerFile and customdoxygen.css
- Add layout file `DoxygenLayout.xml` which used for changing doxygen naviagation (top link bar) title
- Add HTML_HEADER `headerFile` to set a favicon
- Add HTML_EXTRA_STYLESHEET `customdoxygen.css` to set example code font
- Add HTML_TIMESTAMP which will show generaion doxygen file time

v0.0.10.1 Date: 2022/11/09, Developer: Chunglee_people
------------------------------------------------------
### Added
- Add .nojekyll file
- Add `Products Feature` in documentation

v0.0.10 Date: 2022/11/08, Developer: Chunglee_people
----------------------------------------------------
### Added
- Decorate doxygen with example code
- Add `examples` folder which contains module example code
- Add Reference folder which contains Datasheet & Manuals & Pinouts & Products & WPC_error_code.csv

v0.0.5 Date: 2022/10/19, Developer: Chunglee_people
----------------------------------------------------
### Added
- Add example_dev with portal
- Add Console-AI example code `example_AI_on_demand_in_loop`
- Add example_AI_continuous.cs example_AI_N_samples_in_loop.cs example_get_WifiDAQ_status.cs

### Changed
- Change WPC C# Version from 0.0.4 to 0.0.5

v0.0.4 Date: 2022/10/05, Developer: Chunglee_people
----------------------------------------------------
### Added
- Add Console-DO example code `example_DIO_loopback_pins` & `example_DIO_loopback_port`
& `example_AI_on_demand_in_loop`

### Changed
- Change WPC C# Version from 0.0.3 to 0.0.4


v0.0.3 Date: 2022/09/30, Developer: Chunglee_people
----------------------------------------------------
### Added
- Add Console-AI example code `example_AI_N_samples_once` & `example_AI_on_demand_once`
& `example_AI_on_demand_in_loop`

### Changed
- Change WPC C# Version from 0.0.2 to 0.0.3

v0.0.2 Date: 2022/09/26, Developer: Chunglee_people
---------------------------------------------------
### Added
- Add Console-System example code `example_find_all_devices` & `example_get_device_info`

### Changed
- Change WPC C# Version from 0.0.1 to 0.0.2